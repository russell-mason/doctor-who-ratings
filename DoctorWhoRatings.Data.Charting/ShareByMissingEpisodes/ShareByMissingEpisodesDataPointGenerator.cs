namespace DoctorWhoRatings.Data.Charting.ShareByMissingEpisodes;

public class ShareByMissingEpisodesDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IShareByMissingEpisodesDataPointGenerator
{
    public List<ShareByMissingEpisodesDataPoint> Generate(ShareByMissingEpisodesDataOptions options)
    {
        var episodes = dataProvider.DoctorWhoData.Episodes.ToList();

        var dataPoints = options.CalculationMethod switch
        {
            ShareByMissingEpisodesCalculationMethod.All => CreateAllDataPoints(episodes),

            ShareByMissingEpisodesCalculationMethod.Doctor => CreateDoctorDataPoints(episodes),

            ShareByMissingEpisodesCalculationMethod.Season => CreateSeasonDataPoints(episodes),

            _ => throw new ArgumentOutOfRangeException(nameof(options.CalculationMethod))
        };

        return dataPoints;
    }

    private static List<ShareByMissingEpisodesDataPoint> CreateAllDataPoints(List<Episode> episodes)
    {
        var totalCount = episodes.Count;
        var missingCount = episodes.Count(episode => episode.IsMissing);
        var availableCount = totalCount - missingCount;

        var missingDataPoint = new ShareByMissingEpisodesDataPoint
        {
            Description = "Missing",
            TotalEpisodeCount = totalCount,
            ContextEpisodeCount = totalCount,
            EpisodeCount = missingCount,
            CalculatedTotalPercentageShare = 100,
            CalculatedContextPercentageShare = Math.Round((decimal) missingCount / totalCount * 100, 2)
        };

        var availableDataPoint = new ShareByMissingEpisodesDataPoint
        {
            Description = "Available",
            TotalEpisodeCount = totalCount,
            ContextEpisodeCount = totalCount,
            EpisodeCount = availableCount,
            CalculatedTotalPercentageShare = 100,
            CalculatedContextPercentageShare = Math.Round((decimal) availableCount / totalCount * 100, 2)
        };

        return [missingDataPoint, availableDataPoint];
    }

    private static List<ShareByMissingEpisodesDataPoint> CreateDoctorDataPoints(List<Episode> episodes) =>
        CreateSeasonDataPoints(episodes, episode => $"{episode.Doctor}", episode => episode.Actor);

    // Season may repeat so ensure they can be distinguished by including the era in the grouping key
    private static List<ShareByMissingEpisodesDataPoint> CreateSeasonDataPoints(List<Episode> episodes) =>
        CreateSeasonDataPoints(episodes, episode => $"{episode.Season}-{episode.EraId}", episode => episode.SeasonDescription);

    private static List<ShareByMissingEpisodesDataPoint> CreateSeasonDataPoints(List<Episode> episodes,
                                                                                Func<Episode, string> selector,
                                                                                Func<Episode, string> actorSelector)
    {
        var dataPoints = episodes.GroupBy(selector)
                                 .Where(group => group.Count(episode => episode.IsMissing) > 0)
                                 .Select(group => CreateDataPoint(group, actorSelector(group.First())))
                                 .ToList();

        return dataPoints;

        ShareByMissingEpisodesDataPoint CreateDataPoint(IGrouping<string, Episode> group, string description)
        {
            var totalCount = episodes.Count(episode => episode.IsMissing);
            var contextCount = group.Count();
            var count = group.Count(episode => episode.IsMissing);

            var dataPoint = new ShareByMissingEpisodesDataPoint
            {
                Description = description,
                TotalEpisodeCount = totalCount,
                ContextEpisodeCount = contextCount,
                EpisodeCount = count,
                CalculatedTotalPercentageShare = Math.Round((decimal) count / totalCount * 100, 2),
                CalculatedContextPercentageShare = Math.Round((decimal) count / contextCount * 100, 2)
            };

            return dataPoint;
        }
    }
}
