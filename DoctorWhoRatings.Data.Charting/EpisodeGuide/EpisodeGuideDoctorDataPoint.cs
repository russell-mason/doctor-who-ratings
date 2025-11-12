namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDoctorDataPoint
{
    public int Doctor { get; init; }

    public required string Actor { get; init; }

    public required List<EpisodeGuideSeasonDataPoint> Seasons { get; init; }

    public bool HasMissingEpisodes { get; init; }
}