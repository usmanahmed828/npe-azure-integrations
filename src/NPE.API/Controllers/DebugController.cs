using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPE.Infrastructure;

namespace NPE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DebugController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            var center =
                await _context.Centers
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == 57);

            var consultant =
                await _context.Consultants
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == 5);

            var reference =
                await _context.References
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == 12);

            return Ok(new
            {
                CenterStatus = center.Status,
                ConsultantStatus = consultant.Status,
                ReferenceStatus = reference.Status
            });
        }

        [HttpGet("consultant-test")]
        public async Task<IActionResult> ConsultantTest()
        {
            var allConsultants =
                await _context.Consultants
                    .AsNoTracking()
                    .ToListAsync();

            var activeConsultants =
                await _context.Consultants
                    .AsNoTracking()
                    .Where(x => x.Status)
                    .ToListAsync();

            return Ok(new
            {
                TotalConsultants =
                    allConsultants.Count,

                ActiveConsultants =
                    activeConsultants.Count,

                Consultant5 =
                    allConsultants
                        .FirstOrDefault(x => x.Id == 5),

                Consultant5WithFilter =
                    activeConsultants
                        .FirstOrDefault(x => x.Id == 5)
            });
        }

        [HttpGet("reference-test")]
        public async Task<IActionResult> ReferenceTest()
        {
            var allReferences =
                await _context.References
                    .AsNoTracking()
                    .ToListAsync();

            var activeReferences =
                await _context.References
                    .AsNoTracking()
                    .Where(x => x.Status)
                    .ToListAsync();

            return Ok(new
            {
                TotalReferences =
                    allReferences.Count,

                ActiveReferences =
                    activeReferences.Count,

                Reference12 =
                    allReferences
                        .FirstOrDefault(x => x.Id == 12),

                Reference12WithFilter =
                    activeReferences
                        .FirstOrDefault(x => x.Id == 12)
            });
        }

        [HttpGet("status-test")]
        public async Task<IActionResult> StatusTest()
        {
            return Ok(new
            {
                Centers =
                    await _context.Centers
                        .AsNoTracking()
                        .Where(x => x.Status)
                        .CountAsync(),

                Consultants =
                    await _context.Consultants
                        .AsNoTracking()
                        .Where(x => x.Status)
                        .CountAsync(),

                References =
                    await _context.References
                        .AsNoTracking()
                        .Where(x => x.Status)
                        .CountAsync()
            });
        }
    }
}
