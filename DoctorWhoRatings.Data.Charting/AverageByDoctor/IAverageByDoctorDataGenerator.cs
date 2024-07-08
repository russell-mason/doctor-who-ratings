namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

/// <summary>
/// Generates a chart-based data source specifically designed to show the average, or median, ratings across all
/// episodes for each doctor.
/// </summary>
public interface IAverageByDoctorDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the average ratings are calculated.</param>
    /// <returns>A specific structure containing calculated average data.</returns>
    List<AverageByDoctorDataPoint> Generate(AverageByDoctorDataOptions options);
}