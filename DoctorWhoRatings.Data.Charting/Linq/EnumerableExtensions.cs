namespace DoctorWhoRatings.Data.Charting.Linq;

public static class EnumerableExtensions
{
    /// <summary>
    /// Gets the central element of a sequence of values.
    /// <para>
    /// This can provide a more representative value than an average.
    /// </para>
    /// </summary>
    /// <param name="source">An enumerable list of values. There must be at least one value in the list.</param>
    /// <returns>The median value.</returns>
    public static decimal Median(this IEnumerable<decimal> source)
    {
        var sorted = source.OrderBy(value => value).ToList();
        var count = sorted.Count;

        if (count == 0)
        {
            throw new InvalidOperationException("The source sequence is empty.");
        }

        return count % 2 == 0 ? (sorted[count / 2 - 1] + sorted[count / 2]) / 2 : sorted[count / 2];
    }

    /// <summary>
    /// Calculates the sum of a sequence of values.
    /// <para>
    /// If all the values are null then the result is null. If only some of the values are null, then those values
    /// will be treated as zero.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of object that the transform selector is executed against.</typeparam>
    /// <param name="source">An enumerable list of values. There must be at least one value in the list.</param>
    /// <param name="selector">The transform selector to determine the value by.</param>
    /// <returns>The sum of all values, or null if all values are null.</returns>
    public static decimal? SumOrDefault<T>(this IEnumerable<T> source, Func<T, decimal?> selector)
    {
        var list = source.ToList();

        return list.Any(value => selector(value) != null) ? list.Sum(selector) : null;
    }
}
