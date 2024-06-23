namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesData
{
    public const string Title = "All Episodes";

    public const string Description = "Shows ratings for all episodes, including Classic Who, New Who, and the Movie.";

    public const string XAxisTitle = "Episodes";

    public const string YAxisTitle = "Ratings (millions)";

    public IReadOnlyList<EpisodeDataPoint> DataPoints { get; set; } = [];
}