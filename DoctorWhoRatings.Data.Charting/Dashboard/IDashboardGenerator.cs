namespace DoctorWhoRatings.Data.Charting.Dashboard;

/// <summary>
/// Generates a chart-based data source specifically designed to show overview information for a dashboard.
/// </summary>
public interface IDashboardGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing calculated data providing general overviews.</returns>
    DashboardData Generate();
}
