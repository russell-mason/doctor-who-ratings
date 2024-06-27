namespace DoctorWhoRatings.Data.Charting.AllEpisodes;

public class AllEpisodesDataGenerator(IDoctorWhoDataProvider dataProvider) : IAllEpisodesDataGenerator
{
    public AllEpisodesData Generate()
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Select(CreateDataPoint)
                                     .ToList()
                                     .AsReadOnly();

        var allEpisodesData = new AllEpisodesData
        {
            DataPoints = dataPoints
        };

        return allEpisodesData;
    }

    private static AllEpisodesDataPoint CreateDataPoint(Episode episode)
    {
        var consolidatedExcessRatings = episode.ConsolidatedRatings - episode.OvernightRatings;
        var extendedExcessRatings = episode.ExtendedRatings - episode.ConsolidatedRatings;
        var adjustedConsolidatedExcess = episode.PopulationAdjustedConsolidatedRatings - episode.PopulationAdjustedOvernightRatings;
        var adjustedExtendedExcess = episode.PopulationAdjustedExtendedRatings - episode.PopulationAdjustedConsolidatedRatings;

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
            ConsolidatedExcessRatings = consolidatedExcessRatings,
            ExtendedRatings = episode.ExtendedRatings,
            ExtendedExcessRatings = extendedExcessRatings,
            PopulationAdjustedOvernightRatings = episode.PopulationAdjustedOvernightRatings,
            PopulationAdjustedConsolidatedRatings = episode.PopulationAdjustedConsolidatedRatings,
            PopulationAdjustedExtendedRatings = episode.PopulationAdjustedExtendedRatings,
            PopulationAdjustedConsolidatedExcessRatings = adjustedConsolidatedExcess,
            PopulationAdjustedExtendedExcessRatings = adjustedExtendedExcess,
            PopulationFactor = episode.PopulationFactor,
            Note = episode.Note
        };

        return dataPoint;
    }
}
