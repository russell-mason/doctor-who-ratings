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
            PopulationFactor = episode.PopulationFactor,
            Note = episode.Note
        };

        return dataPoint;
    }
}
