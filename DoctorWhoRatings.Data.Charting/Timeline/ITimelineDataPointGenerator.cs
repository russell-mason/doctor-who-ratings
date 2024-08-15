namespace DoctorWhoRatings.Data.Charting.Timeline;

/// <summary>
/// Generates a chart-based data source specifically designed to show when each Doctor was introduced.
/// </summary>
public interface ITimelineDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing a timeline of doctors.</returns>
    List<TimelineDataPoint> Generate();
}