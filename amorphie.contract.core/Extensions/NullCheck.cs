using System.Diagnostics.CodeAnalysis;

namespace amorphie.contract.core.Extensions;

public static class NullCheck
{
    public static bool IsNotEmpty<T>([NotNullWhen(true)] this List<T>? list)
    {
        return list is not null && list.Count() > 0;
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
    {
        return enumerable ?? Enumerable.Empty<T>();
    }
}