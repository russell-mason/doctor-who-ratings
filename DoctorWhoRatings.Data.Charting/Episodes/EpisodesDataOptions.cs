namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataOptions
{
    public int? IdFilter { get; set; }

    public string? SlugFilter { get; set; }

    public int? EraId { get; set; }

    public int? DoctorFilter { get; set; }

    public int? SeasonFilter { get; set; }

    public int? StoryFilter { get; set; }

    public int? EpisodeFormatIdFilter { get; set; }

    public Predicate<Episode>? CustomFilter { get; set; }

    public bool AdjustForCurrentPopulation { get; set; } = false;
}