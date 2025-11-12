namespace DoctorWhoRatings.Data.Charting.Top20Writers;

public class Top20WritersDataPointGenerator(IDoctorWhoDataProvider dataProvider) : ITop20WritersDataPointGenerator
{
    public List<Top20WritersDataPoint> Generate(Top20WritersDataOptions options)
    {
        var writers = dataProvider.DoctorWhoData.Writers.Where(writer => !writer.IsAlias).ToList();

        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .SelectMany(episode => episode.WriterIds
                                                                   .Select(writerId => new
                                                                                       {
                                                                                           Writer = writers.First(writer => writer.Id == writerId),
                                                                                           Episode = episode
                                                                                       }))
                                     .GroupBy(writerEpisode => writerEpisode.Writer, writerEpisode => writerEpisode.Episode)
                                     .Select(group => CreateDataPoint(group.Key, group.ToList(), options))
                                     .OrderByDescending(dataPoint => dataPoint.CalculatedByOptions)
                                     .Take(20)
                                     .ToList();

        return dataPoints;
    }

    private static Top20WritersDataPoint CreateDataPoint(Writer writer, List<Episode> episodes, Top20WritersDataOptions options)
    {
        const int minutesInHour = 60;

        var aliases = episodes.Select(episode => episode.WriterAlias).Distinct().ToArray();

        var episodeCount = episodes.Count;
        var totalEpisodeHours = episodes.Sum(episode => (decimal) episode.Runtime) / minutesInHour;
        var totalOvernightHours = episodes.SumOrDefault(GetOvernightMinutes) / minutesInHour;
        var totalConsolidatedHours = episodes.SumOrDefault(GetConsolidatedMinutes) / minutesInHour;
        var totalExtendedHours = episodes.SumOrDefault(GetExtendedMinutes) / minutesInHour;

        var calculatedByOptions = options.CalculationMethod switch
        {
            Top20WritersCalculationMethod.NumberOfEpisodes => episodes.Count,

            Top20WritersCalculationMethod.ScriptHours => totalEpisodeHours,

            Top20WritersCalculationMethod.OvernightHoursWatched => totalOvernightHours,

            Top20WritersCalculationMethod.ConsolidatedHoursWatched => totalConsolidatedHours,

            Top20WritersCalculationMethod.ExtendedHoursWatched => totalExtendedHours,

            _ => 0
        };

        var highestOvernightRating = episodes.Max(episode => episode.OvernightRatings) ?? 0;
        var lowestOvernightRating = episodes.Min(episode => episode.OvernightRatings) ?? 0;

        var dataPoint = new Top20WritersDataPoint
        {
            Id = writer.Id,
            Name = writer.Name,
            Aliases = aliases,
            EpisodeCount = episodeCount,
            TotalEpisodeHours = totalEpisodeHours,
            TotalOvernightHoursWatched = totalOvernightHours,
            TotalConsolidatedHoursWatched = totalConsolidatedHours,
            TotalConsolidatedExcessHoursWatched = totalConsolidatedHours - totalOvernightHours,
            TotalExtendedHoursWatched = totalExtendedHours,
            TotalExtendedExcessHoursWatched = totalExtendedHours - totalConsolidatedHours,
            CalculatedByOptions = calculatedByOptions,
            HighestOvernightRating = highestOvernightRating,
            LowestOvernightRating = lowestOvernightRating
        };

        return dataPoint;
    }

    public static decimal? GetOvernightMinutes(Episode episode) => episode.OvernightRatings * episode.Runtime;

    public static decimal? GetConsolidatedMinutes(Episode episode) => 
        (episode.ConsolidatedRatings ?? episode.OvernightRatings) * episode.Runtime;

    public static decimal? GetExtendedMinutes(Episode episode) => 
        (episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings) * episode.Runtime;
}
