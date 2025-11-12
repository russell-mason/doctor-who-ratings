namespace DoctorWhoRatings.Data.Charting.Dashboard;

public class DashboardGenerator(IDoctorWhoDataProvider dataProvider) : IDashboardGenerator
{
    public DashboardData Generate()
    {
        const int minutesInHour = 60;

        var episodes = dataProvider.DoctorWhoData.Episodes;

        var data = new DashboardData 
        {
            PeriodSinceFirstEpisode = episodes[0].OriginalAirDate.PeriodToNow(),
            PeriodSinceLastEpisode = episodes[^1].OriginalAirDate.PeriodToNow(),
            NumberOfDoctors = dataProvider.DoctorWhoData.Doctors.Count,
            NumberOfSeasons = episodes.Where(episode =>episode.Season.HasValue).DistinctBy(episode => episode.SeasonDescription).Count(),
            TotalNumberOfEpisodes = episodes.Count,
            NumberOfStories = episodes[^1].Story,
            NumberOfSeriesEpisodes = episodes.Count(episode => episode.EpisodeFormatId == EpisodeFormats.Series),
            NumberOfMissingEpisodes = episodes.Count(episode => episode.IsMissing),
            NumberOfMovies =episodes.Count(episode => episode.EpisodeFormatId == EpisodeFormats.Movie),
            NumberOfSpecials = episodes.Count(episode => episode.EpisodeFormatId == EpisodeFormats.Special),
            TotalEpisodeHours = (decimal) episodes.Sum(episode => episode.Runtime) / minutesInHour,
            TotalHoursWatched = (CalculateMinutesWatched(episodes) ?? 0) / minutesInHour,
            TotalRatings = CalculateRatings(episodes) ?? 0
        };

        return data;
    }

    private static decimal? CalculateRatings(IEnumerable<Episode> episodes) =>
        episodes.Sum(episode => episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings);

    private static decimal? CalculateMinutesWatched(IEnumerable<Episode> episodes) =>
        episodes.Sum(episode => (episode.Runtime * (episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings)));
}
