namespace DoctorWhoRatings.Data.Charting.Top20Episodes;

public class Top20EpisodesDataPointGenerator(IDoctorWhoDataProvider dataProvider) : ITop20EpisodesDataPointGenerator
{
    public List<Top20EpisodesDataPoint> Generate(Top20EpisodesDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => IsFormatIncluded(episode, options))
                                     .OrderByDescending(episode => SelectRating(episode, options))
                                     .Take(20)
                                     .Select(episode => CreateDataPoint(episode, options))
                                     .ToList();

        return dataPoints;
    }

    private static Top20EpisodesDataPoint CreateDataPoint(Episode episode, Top20EpisodesDataOptions options)
    {
        var dataPoint = new Top20EpisodesDataPoint
        {
            Id = episode.Id,
            Actor = episode.Actor,
            Season = episode.Season,
            SeasonFormatDescription = episode.SeasonFormatDescription,
            StoryTitle = episode.StoryTitle,
            PartTitle = episode.PartTitle,
            Slug = episode.Slug,
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
            CalculatedOvernightRatings = SelectOvernightRatings(episode, options),
            CalculatedConsolidatedRatings = SelectConsolidatedRatings(episode, options),
            CalculatedConsolidatedExcessRatings = SelectConsolidatedExcessRatings(episode, options),
            CalculatedExtendedRatings = SelectExtendedRatings(episode, options),
            CalculatedExtendedExcessRatings = SelectExtendedExcessRatings(episode, options)
        };

        return dataPoint;
    }

    private static bool IsFormatIncluded(Episode episode, Top20EpisodesDataOptions options) =>
        options.IncludeSpecials || episode.EpisodeFormatId == EpisodeFormats.Series;

    private static decimal? SelectRating(Episode episode, Top20EpisodesDataOptions options)
    {
        var result = options.CalculationMethod switch
        {
            Top20EpisodesCalculationMethod.Overnight => SelectOvernightRatings(episode, options),
            Top20EpisodesCalculationMethod.Consolidated => SelectConsolidatedRatings(episode, options)
                                                           ?? SelectOvernightRatings(episode, options),
            Top20EpisodesCalculationMethod.Extended => SelectExtendedRatings(episode, options)
                                                       ?? SelectConsolidatedRatings(episode, options)
                                                       ?? SelectOvernightRatings(episode, options),
            _ => throw new ArgumentOutOfRangeException(nameof(options))
        };

        return result;
    }

    private static decimal? SelectOvernightRatings(Episode episode, Top20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedOvernightRatings : episode.OvernightRatings;

    private static decimal? SelectConsolidatedRatings(Episode episode, Top20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedRatings : episode.ConsolidatedRatings;

    private static decimal? SelectConsolidatedExcessRatings(Episode episode, Top20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedExcessRatings : episode.ConsolidatedExcessRatings;

    private static decimal? SelectExtendedRatings(Episode episode, Top20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedRatings : episode.ExtendedRatings;

    private static decimal? SelectExtendedExcessRatings(Episode episode, Top20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedExcessRatings : episode.ExtendedExcessRatings;
}
