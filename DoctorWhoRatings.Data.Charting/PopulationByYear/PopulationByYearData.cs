namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

public class PopulationByYearData
{
    public const string Title = "UK Population";

    public const string Description = "Shows the population of the UK from 1963 to present day.";

    public const string XAxisTitle = "Year";

    public const string YAxisTitle = "Population (million)";

    public static readonly string[] SeriesTitles = ["Year"];

    public IReadOnlyList<PopulationByYearDataPoint> DataPoints { get; set; } = [];
}