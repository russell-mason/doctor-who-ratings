namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDataOptions
{
    public string Filter { get; set; } = string.Empty;

    public MissingEpisodeHandling MissingEpisodeHandling { get; set; }
}