namespace DoctorWhoRatings.Data.Charting.Episodes;

public class EpisodesDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IEpisodesDataPointGenerator
{
    public List<EpisodeDataPoint> Generate(EpisodesDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => MatchesFilter(episode, options))
                                     .Select(CreateDataPoint)
                                     .ToList();

        return dataPoints;
    }

    private static bool MatchesFilter(Episode episode, EpisodesDataOptions options)
    {
        return options.DoctorFilter == null || episode.Doctor == options.DoctorFilter;
    }

    private static EpisodeDataPoint CreateDataPoint(Episode episode)
    {
        var dataPoint = new EpisodeDataPoint
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
