namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

public class PopulationByYearDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IPopulationByYearDataPointGenerator
{
    public List<PopulationByYearDataPoint> Generate() => dataProvider.DoctorWhoData.Populations
                                                                     .Select(CreateDataPoint)
                                                                     .ToList();

    private static PopulationByYearDataPoint CreateDataPoint(YearPopulation yearPopulation) =>
        new()
        {
            Year = yearPopulation.Year.ToString(),
            Population = yearPopulation.Population,
            Note = yearPopulation.Note
        };
}