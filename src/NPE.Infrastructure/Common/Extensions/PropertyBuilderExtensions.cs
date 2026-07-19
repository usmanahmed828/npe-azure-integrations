using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NPE.Infrastructure.Common.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<bool>
        HasNexusStatusConversion(
            this PropertyBuilder<bool> builder)
    {
        var converter =
            new ValueConverter<bool, bool>(
                toDb => !toDb,
                fromDb => !fromDb);

        return builder.HasConversion(
            converter);
    }

    public static PropertyBuilder<bool?>
        HasNexusStatusConversion(
            this PropertyBuilder<bool?> builder)
    {
        var converter =
            new ValueConverter<bool?, bool?>(
                toDb => toDb == null
                    ? null
                    : !toDb.Value,

                fromDb => fromDb == null
                    ? null
                    : !fromDb.Value);

        return builder.HasConversion(
            converter);
    }
}