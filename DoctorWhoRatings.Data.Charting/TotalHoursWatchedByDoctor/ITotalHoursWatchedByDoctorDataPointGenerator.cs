namespace DoctorWhoRatings.Data.Charting.TotalHoursWatchedByDoctor;

/// <summary>
/// Generates a chart-based data source specifically designed to show the total number of hours watched for each doctor.
/// </summary>
public interface ITotalHoursWatchedByDoctorDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the total hours are calculated.</param>
    /// <returns>A specific structure containing hours watched data.</returns>
    List<TotalHoursWatchedByDoctorDataPoint> Generate(TotalHoursWatchedByDoctorDataOptions options);
}