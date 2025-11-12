namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataOptions
{
    public int? IdFilter { get; set; } = null;

    public string? SlugFilter { get; set; } = null;

    public int? EraId { get; set; } = null;

    public int? DoctorFilter { get; set; } = null;

    public int? SeasonFilter { get; set; } = null;

    public int? StoryFilter { get; set; } = null;

    public int? EpisodeFormatIdFilter { get; set; } = null;

    public Predicate<Episode>? CustomFilter { get; set; } = null;

    public bool AdjustForCurrentPopulation { get; set; } = false;
}