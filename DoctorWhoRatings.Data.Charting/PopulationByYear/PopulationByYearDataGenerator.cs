namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

public class PopulationByYearDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IPopulationByYearDataPointGenerator
{
    public List<PopulationByYearDataPoint> Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Populations
                                     .Select(CreateDataPoint)
                                     .ToList();

        return dataPoints;
    }

    private static PopulationByYearDataPoint CreateDataPoint(YearPopulation yearPopulation)
    {
        var dataPoint = new PopulationByYearDataPoint
        {
            Year = yearPopulation.Year.ToString(),
            Population = yearPopulation.Population,
            Note = yearPopulation.Note
        };

        return dataPoint;
    }
}