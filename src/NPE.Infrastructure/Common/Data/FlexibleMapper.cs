using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace NPE.Infrastructure.Common.Data
{
    public static class FlexibleMapper
    {
        public static List<T> MapToList<T>(
            IDataReader reader)
            where T : new()
        {
            var result = new List<T>();

            var columns = Enumerable
                .Range(0, reader.FieldCount)
                .Select(reader.GetName)
                .ToHashSet(
                    StringComparer.OrdinalIgnoreCase);

            var props = typeof(T)
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Instance);

            while (reader.Read())
            {
                var obj = new T();

                foreach (var prop in props)
                {
                    var columnName =
                        GetColumnName(prop);

                    if (!columns.Contains(columnName))
                        continue;

                    var value =
                        reader[columnName];

                    if (value == DBNull.Value)
                        continue;

                    var targetType =
                        Nullable.GetUnderlyingType(
                            prop.PropertyType)
                        ?? prop.PropertyType;

                    var safeValue =
                        Convert.ChangeType(
                            value,
                            targetType);

                    prop.SetValue(
                        obj,
                        safeValue);
                }

                result.Add(obj);
            }

            return result;
        }

        private static string GetColumnName(
            PropertyInfo prop)
        {
            var attr =
                prop.GetCustomAttribute<ColumnAttribute>();

            return attr?.Name ?? prop.Name;
        }
    }
}