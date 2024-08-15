namespace DoctorWhoRatings.Data.Charting.Dashboard;

public class DashboardGenerator(IDoctorWhoDataProvider dataProvider) : IDashboardGenerator
{
    public DashboardData Generate()
    {
        const int minutesInHour = 60;

        var episodes = dataProvider.DoctorWhoData.Episodes;

        var data = new DashboardData 
        {
            PeriodSinceFirstEpisode = PeriodToNow(episodes[0].OriginalAirDate),
            PeriodSinceLastEpisode = PeriodToNow(episodes[^1].OriginalAirDate),
            NumberOfDoctors = dataProvider.DoctorWhoData.Doctors.Count,
            NumberOfSeasons = episodes.Where(episode =>episode.Season.HasValue).DistinctBy(episode => episode.SeasonDescription).Count(),
            TotalNumberOfEpisodes = episodes.Count,
            NumberOfStories = episodes[^1].Story,
            NumberOfSeriesEpisodes = episodes.Count(episode => episode.EpisodeFormatId == EpisodeFormats.Series),
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

    private static (int years, int months, int days) PeriodToNow(DateTime from)
    {
        var to = DateTime.UtcNow.Date;
        var years = to.Year - from.Year;
        var months = to.Month - from.Month;
        var days = to.Day - from.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(to.Year, to.Month - 1);
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        return (years, months, days);
    } 
}
