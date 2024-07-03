namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

public class Top20EpisodesDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public Top20EpisodesCalculationMethod CalculationMethod { get; set; } = Top20EpisodesCalculationMethod.Overnight;

    public bool IncludeSpecials { get; set; } = true;
}