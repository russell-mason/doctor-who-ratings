namespace DoctorWhoRatings.Data.Charting.Bottom20Episodes;

public class Bottom20EpisodesDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public Bottom20EpisodesCalculationMethod CalculationMethod { get; set; } = Bottom20EpisodesCalculationMethod.Overnight;

    public bool IncludeSpecials { get; set; } = true;
}