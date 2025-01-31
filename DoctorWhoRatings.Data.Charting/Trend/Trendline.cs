namespace DoctorWhoRatings.Data.Charting.Trend;

/// <summary>
/// Adapted from answer by Thymine
/// https://stackoverflow.com/questions/43224/how-do-i-calculate-a-trendline-for-a-graph
/// </summary>
public class Trendline
{
    public Trendline(IEnumerable<(decimal x, decimal y)> data)
    {
        var dataList = data.ToList();

        var pointCount = dataList.Count;
        var sumX = dataList.Sum(point => point.x);
        var sumX2 = dataList.Sum(point => point.x * point.x);
        var sumY = dataList.Sum(point => point.y);
        var sumXY = dataList.Sum(point => point.x * point.y);

        Slope = dataList.Count == 1 ? 0 : (sumXY - ((sumX * sumY) / pointCount)) / (sumX2 - (sumX * sumX / pointCount));
        Intercept = (sumY / pointCount) - (Slope * (sumX / pointCount));

        StartX = dataList.First().x;
        StartY = GetYValue(dataList.Min(point => point.x));

        EndX = dataList.Last().x;
        EndY = GetYValue(dataList.Max(point => point.x));

    }

    public decimal Slope { get; }

    public decimal Intercept { get; }

    public decimal StartX { get; }

    public decimal StartY { get; }

    public decimal EndX { get; }

    public decimal EndY { get; }

    public decimal GetYValue(decimal xValue) => Intercept + Slope * xValue;
}