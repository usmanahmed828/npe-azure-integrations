using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.CaseNumbers;
using NPE.Infrastructure.Modules.Management.Centers;

namespace NPE.Infrastructure.Modules.Cases.Services
{
    public class CaseNumberService : ICaseNumberService
    {
        private readonly ApplicationDbContext _context;

        public CaseNumberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetNextCaseNumberAsync(
            int centerCode,
            DateTime caseDate,
            CancellationToken cancellationToken = default)
        {
            var dateOnly = caseDate.Date;

            var maxLabNo = await GetMaxLabNo(centerCode, cancellationToken);
            var nextLabNo = await GetNextLabNo(centerCode, dateOnly, cancellationToken);

            if (nextLabNo > maxLabNo)
                throw new InvalidOperationException(
                    $"Daily case limit exceeded for Center={centerCode}");

            return Format(nextLabNo, dateOnly);
        }

        public async Task<string> ConsumeAsync(
            int centerCode,
            DateTime caseDate,
            CancellationToken cancellationToken = default)
        {
            var dateOnly = caseDate.Date;

            var maxLabNo = await GetMaxLabNo(centerCode, cancellationToken);

            var labRow = await _context.MaxLabNos
                .FromSqlRaw(@"
                    SELECT *
                    FROM MaxLabNos WITH (UPDLOCK, ROWLOCK)
                    WHERE CenterCode = @Center
                      AND Dated = @dated",
                    new SqlParameter("@Center", centerCode),
                    new SqlParameter("@dated", dateOnly))
                .SingleOrDefaultAsync(cancellationToken);

            int currentLabNo;

            if (labRow == null)
            {
                currentLabNo = centerCode;

                if (currentLabNo > maxLabNo)
                    throw new InvalidOperationException(
                        $"Daily case limit exceeded for Center={centerCode}");

                labRow = new MaxLabNo
                {
                    CenterCode = centerCode,
                    Dated = dateOnly,
                    NextLabNo = centerCode + 1
                };

                _context.MaxLabNos.Add(labRow);
            }
            else
            {
                currentLabNo = labRow.NextLabNo;

                if (currentLabNo > maxLabNo)
                    throw new InvalidOperationException(
                        $"Daily case limit exceeded for Center={centerCode}");

                labRow.NextLabNo += 1;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Format(currentLabNo, dateOnly);
        }

        // ---------------- HELPERS ----------------

        private async Task<int> GetMaxLabNo(int centerCode, CancellationToken ct)
        {
            var max = await _context.CenterLabNos
                .Where(x => x.CenterCode == centerCode)
                .Select(x => x.MaxLabNo)
                .SingleOrDefaultAsync(ct);

            if (max <= 0)
                throw new InvalidOperationException(
                    $"CenterLabNos not configured for Center={centerCode}");

            return max;
        }

        //private async Task<int> GetNextLabNo(int centerCode, DateTime dateOnly, CancellationToken ct)
        //{
        //    var row = await _context.MaxLabNos
        //        .Where(x => x.CenterCode == centerCode && x.Dated == dateOnly)
        //        .Select(x => x.NextLabNo)
        //        .SingleOrDefaultAsync(ct);

        //    return row == 0 ? 1 : row;
        //}
        private async Task<int> GetNextLabNo(
    int centerCode,
    DateTime dateOnly,
    CancellationToken ct)
        {
            // 🔥 Try fetch first
            var row = await _context.MaxLabNos
                .FirstOrDefaultAsync(
                    x => x.CenterCode == centerCode && x.Dated == dateOnly,
                    ct);

            if (row != null)
                return row.NextLabNo;

            // 🔥 NOT FOUND → initialize with centerCode
            var initialValue = centerCode;

            var newRow = new MaxLabNo
            {
                CenterCode = centerCode,
                Dated = dateOnly,
                NextLabNo = initialValue
            };

            _context.MaxLabNos.Add(newRow);

            try
            {
                await _context.SaveChangesAsync(ct);

                return initialValue; // ✅ return centerCode
            }
            catch (DbUpdateException)
            {
                // 🔥 Another request inserted → fetch again
                row = await _context.MaxLabNos
                    .FirstAsync(
                        x => x.CenterCode == centerCode && x.Dated == dateOnly,
                        ct);

                return row.NextLabNo;
            }
        }

        private static string Format(int labNo, DateTime date)
        {
            return $"{labNo}-{date:dd-MM}";
        }
    }
}
