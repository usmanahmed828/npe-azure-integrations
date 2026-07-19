using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace NPE.Infrastructure.Common.Data
{
    public static class DbHelper
    {
        public static void AddParam(DbCommand cmd, string name, object? value)
        {
            if (value != null)
                cmd.Parameters.Add(new SqlParameter(name, value));
            else
                cmd.Parameters.Add(new SqlParameter(name, DBNull.Value));
        }
        public static void AddOutputParam(
    DbCommand cmd,
    string name,
    DbType type)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(param);
        }
    }
}
