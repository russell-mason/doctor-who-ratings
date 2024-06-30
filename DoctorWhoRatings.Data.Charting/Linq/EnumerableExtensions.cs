namespace DoctorWhoRatings.Data.Charting.Linq;

public static class EnumerableExtensions
{
    /// <summary>
    /// Gets the central element of a sequence of values. This can provide a more representative value than an average.
    /// </summary>
    /// <param name="source">An enumerable list of values. There must be at least one value in the list.</param>
    /// <returns></returns>
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
}
