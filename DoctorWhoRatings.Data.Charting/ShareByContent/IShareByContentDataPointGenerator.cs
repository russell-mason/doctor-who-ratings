namespace DoctorWhoRatings.Data.Charting.ShareByContent;

/// <summary>
/// Generates a chart-based data source specifically designed to show the total number of episodes and hours of content for each doctor.
/// </summary>
public interface IShareByContentDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the totals are calculated.</param>
    /// <returns>A specific structure containing content data.</returns>
    List<ShareByContentDataPoint> Generate(ShareByContentDataOptions options);
}