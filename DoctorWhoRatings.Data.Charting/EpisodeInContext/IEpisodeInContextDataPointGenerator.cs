namespace DoctorWhoRatings.Data.Charting.EpisodeInContext;

public interface IEpisodeInContextDataPointGenerator
{
    public EpisodeInContextDataPoint Generate(EpisodeInContextDataOptions options);
}