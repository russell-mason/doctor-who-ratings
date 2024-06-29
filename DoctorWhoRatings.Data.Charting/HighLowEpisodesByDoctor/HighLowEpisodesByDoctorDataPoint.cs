namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public class HighLowEpisodesByDoctorDataPoint
{
    public required string Actor { get; init; }

    public int EpisodeCount { get; init; }

    public required HighLowEpisodeByDoctorDataPoint HighEpisodeDataPoint { get; init; }

    public required HighLowEpisodeByDoctorDataPoint LowEpisodeDataPoint { get; init; }
}
