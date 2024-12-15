namespace DoctorWhoRatings.Data.Charting.SpecialsOnly;

public class SpecialsOnlyDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public bool IncludeChristmas { get; set; } = true;

    public bool IncludeNewYear { get; set; } = true;

    public bool IncludeNonSeasonal { get; set; } = true;
}
