namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public AverageByDoctorCalculationMethod CalculationMethod { get; set; } = AverageByDoctorCalculationMethod.Mean;
}