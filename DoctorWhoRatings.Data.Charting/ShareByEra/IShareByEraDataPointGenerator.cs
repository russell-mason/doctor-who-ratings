namespace DoctorWhoRatings.Data.Charting.ShareByEra;

/// <summary>
/// Generates a chart-based data source specifically designed to show total ratings based on era.
/// </summary>
public interface IShareByEraDataPointGenerator
{
    /// <summary>
    /// Generates the data source using the specified options.
    /// </summary>
    /// <param name="options">Options that determine how the totals should be calculated.</param>
    /// <returns>A specific structure containing era data.</returns>
    List<ShareByEraDataPoint> Generate(ShareByEraDataOptions options);
}