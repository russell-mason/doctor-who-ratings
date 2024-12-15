namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataOptions
{
    public int? DoctorFilter { get; set; }

    public int? EpisodeFormatIdFilter { get; set; }

    public Predicate<Episode>? CustomFilter { get; set; }

    public bool AdjustForCurrentPopulation { get; set; } = false;
}