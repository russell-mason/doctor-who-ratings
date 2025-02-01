namespace DoctorWhoRatings.Data.Charting.AverageBySeason;

public class AverageBySeasonDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public AverageBySeasonCalculationMethod CalculationMethod { get; set; } = AverageBySeasonCalculationMethod.Mean;
}