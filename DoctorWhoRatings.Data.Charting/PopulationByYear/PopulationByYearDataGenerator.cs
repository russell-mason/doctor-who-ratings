namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

public class PopulationByYearDataGenerator(IDoctorWhoDataProvider dataProvider) : IPopulationByYearDataGenerator
{
    public PopulationByYearData Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Populations
                                     .Select(yearPopulation => new PopulationByYearDataPoint
                                     {
                                         Year = yearPopulation.Year.ToString(),
                                         Population = yearPopulation.Population,
                                         Note = yearPopulation.Note
                                     })
                                     .ToList()
                                     .AsReadOnly();

        var populationByYearData = new PopulationByYearData()
        {
            DataPoints = dataPoints
        };

        return populationByYearData;
    }
}