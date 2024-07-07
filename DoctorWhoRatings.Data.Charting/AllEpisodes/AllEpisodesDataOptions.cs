namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesDataOptions
{
    public int? DoctorFilter { get; set; }

    public bool AdjustForCurrentPopulation { get; set; } = false;
}