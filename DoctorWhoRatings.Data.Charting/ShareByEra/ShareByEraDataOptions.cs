namespace DoctorWhoRatings.Data.Charting.ShareByEra;

public class ShareByEraDataOptions
{
    public ShareByEraCalculationMethod CalculationMethod { get; set; } = ShareByEraCalculationMethod.Sum;

    public ShareByEraOverCalculationMethod OverCalculationMethod { get; set; } = ShareByEraOverCalculationMethod.Overnight;
}