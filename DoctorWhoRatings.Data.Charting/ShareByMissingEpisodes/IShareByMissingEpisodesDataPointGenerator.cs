namespace DoctorWhoRatings.Data.Charting.ShareByMissingEpisodes;

/// <summary>
/// Generates a chart-based data source specifically designed to show the proportion of missing episodes.
/// </summary>
public interface IShareByMissingEpisodesDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the totals should be calculated.</param>
    /// <returns>A specific structure containing missing episode data.</returns>
    List<ShareByMissingEpisodesDataPoint> Generate(ShareByMissingEpisodesDataOptions options);
}