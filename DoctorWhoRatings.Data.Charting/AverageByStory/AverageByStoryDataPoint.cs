namespace DoctorWhoRatings.Data.Charting.AverageByStory;

public class AverageByStoryDataPoint
{
    public int Id { get; init; }

    public required string Actor { get; init; }

    public required string StoryTitle { get; init; }

    public int Season { get; init; }

    public required string SeasonFormatDescription { get; init; }

    public DateTime OriginalAirDate { get; init; }

    public int EpisodeCount { get; init; }

    public decimal? CalculatedOvernightRatings { get; init; }

    public decimal? CalculatedPopulationAdjustedOvernightRatings { get; init; }
}
