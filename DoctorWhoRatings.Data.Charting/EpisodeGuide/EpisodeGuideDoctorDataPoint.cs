namespace DoctorWhoRatings.Data.Charting.EpisodeGuide;

public class EpisodeGuideDoctorDataPoint
{
    public required int Doctor { get; init; }

    public required string Actor { get; init; }

    public required List<EpisodeGuideSeasonDataPoint> Seasons { get; init; }
}