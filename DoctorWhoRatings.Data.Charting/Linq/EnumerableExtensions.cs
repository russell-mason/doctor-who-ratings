namespace DoctorWhoRatings.Data.Charting.Linq;

public static class EnumerableExtensions
{
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
