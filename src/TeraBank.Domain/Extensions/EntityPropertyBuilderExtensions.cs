using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeraBank.Domain.Extensions;

internal static class EntityPropertyBuilderExtensions
{
    internal static PropertyBuilder<T> HasEnumToStringConversion<T>(this PropertyBuilder<T> builder)
        where T : struct, Enum
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.HasConversion(new EnumToStringConverter<T>());

        return builder;
    }
}
