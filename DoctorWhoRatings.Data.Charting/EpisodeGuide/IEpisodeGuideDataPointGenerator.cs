namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

/// <summary>
/// Generates a chart-based data source specifically designed to show a list of episodes.
/// </summary>
public interface IEpisodeGuideDataPointGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing a list of episodes.</returns>
    List<EpisodeGuideDoctorDataPoint> Generate();
}