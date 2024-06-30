namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

/// <summary>
/// Generates a chart-based data source specifically designed to show the highest, and lowest, rated episodes for
/// each doctor.
/// </summary>
public interface IHighLowEpisodesByDoctorDataGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the ratings values are calculated.</param>
    /// <returns>A specific structure containing calculated high/low data.</returns>
    HighLowEpisodesByDoctorData Generate(HighLowEpisodesByDoctorDataOptions options);
}