namespace DoctorWhoRatings.Data.Charting.PopulationByYear;

public class PopulationByYearDataPoint
{
    public required string Year { get; init; }

    public int Population { get; init; }

    public string? Note { get; init; }
}
