namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesDataGenerator(IDoctorWhoDataProvider dataProvider) : IAllEpisodesDataGenerator
{
    public AllEpisodesData Generate(AllEpisodesDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => MatchesFilter(episode, options))
                                     .Select(CreateDataPoint)
                                     .ToList()
                                     .AsReadOnly();

        var allEpisodesData = new AllEpisodesData
        {
            DataPoints = dataPoints
        };

        return allEpisodesData;
    }

    private static bool MatchesFilter(Episode episode, AllEpisodesDataOptions options)
    {
        return options.DoctorFilter == null || episode.Doctor == options.DoctorFilter;
    }

    private static AllEpisodesDataPoint CreateDataPoint(Episode episode)
    {
        var dataPoint = new AllEpisodesDataPoint
        {
            Id = episode.Id,
            Actor = episode.Actor,
            Season = episode.Season,
            SeasonFormatDescription = episode.SeasonFormatDescription,
            StoryTitle = episode.StoryTitle,
            PartTitle = episode.PartTitle,
            OriginalAirDate = episode.OriginalAirDate,
            OvernightRatings = episode.OvernightRatings,
            ConsolidatedRatings = episode.ConsolidatedRatings,
            ConsolidatedExcessRatings = episode.ConsolidatedExcessRatings,
            ExtendedRatings = episode.ExtendedRatings,
            ExtendedExcessRatings = episode.ExtendedExcessRatings,
            PopulationAdjustedOvernightRatings = episode.PopulationAdjustedOvernightRatings,
            PopulationAdjustedConsolidatedRatings = episode.PopulationAdjustedConsolidatedRatings,
            PopulationAdjustedExtendedRatings = episode.PopulationAdjustedExtendedRatings,
            PopulationAdjustedConsolidatedExcessRatings = episode.PopulationAdjustedConsolidatedExcessRatings,
            PopulationAdjustedExtendedExcessRatings = episode.PopulationAdjustedExtendedExcessRatings,
            Note = episode.Note
        };

        return dataPoint;
    }
}
