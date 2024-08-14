namespace DoctorWhoRatings.Data.Charting.ByEra;

public class ByEraDataOptions
{
    public ByEraCalculationMethod CalculationMethod { get; set; } = ByEraCalculationMethod.Sum;

    public ByEraOverCalculationMethod OverCalculationMethod { get; set; } = ByEraOverCalculationMethod.Overnight;
}