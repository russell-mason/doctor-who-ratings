namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

/// <summary>
/// Generates a chart-based data source specifically designed to show the UK population since Doctor Who started.
/// </summary>
public interface IPopulationByYearDataGenerator
{
    /// <summary>
    /// Generates the data source.
    /// </summary>
    /// <returns>A specific structure containing population data.</returns>
    PopulationByYearData Generate();
}