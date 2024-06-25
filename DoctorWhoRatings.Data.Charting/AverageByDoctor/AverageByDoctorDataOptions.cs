namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public CalculationMethod CalculationMethod { get; set; } = CalculationMethod.Mean;
}