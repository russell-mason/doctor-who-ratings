namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideSeasonDataPoint
{
    public int? Season { get; init; }

    public int SeasonFormatId { get; set; }

    public required string SeasonFormatDescription { get; init; }

    public required List<EpisodeGuideStoryDataPoint> Stories { get; init; }
}