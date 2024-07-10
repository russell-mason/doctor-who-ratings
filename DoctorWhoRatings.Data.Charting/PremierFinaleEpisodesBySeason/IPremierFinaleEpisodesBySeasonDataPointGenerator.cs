namespace DoctorWhoRatings.Data.Charting.PremierFinaleEpisodesBySeason;

/// <summary>
/// Generates a chart-based data source specifically designed to show the first, and last, episode ratings for
/// each season.
/// </summary>
public interface IPremierFinaleEpisodesBySeasonDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the ratings values are calculated.</param>
    /// <returns>A specific structure containing the calculated premier/finale data.</returns>
    Dictionary<string, List<PremierFinaleEpisodeBySeasonDataPoint>> Generate(PremierFinaleEpisodesBySeasonDataOptions options);
}