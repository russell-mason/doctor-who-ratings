namespace DoctorWhoRatings.Data.Charting.Bottom20Episodes;

public class Bottom20EpisodesDataGenerator(IDoctorWhoDataProvider dataProvider) : IBottom20EpisodesDataGenerator
{
    public Bottom20EpisodesData Generate(Bottom20EpisodesDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => IsFormatIncluded(episode, options))
                                     .OrderBy(episode => SelectRating(episode, options))
                                     .Take(20)
                                     .Select(episode => CreateDataPoint(episode, options))
                                     .ToList()
                                     .AsReadOnly();

        var bottom20EpisodesData = new Bottom20EpisodesData
        {
            DataPoints = dataPoints
        };

        return bottom20EpisodesData;
    }

    private static Bottom20EpisodesDataPoint CreateDataPoint(Episode episode, Bottom20EpisodesDataOptions options)
    {
        var dataPoint = new Bottom20EpisodesDataPoint
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
            CalculatedOvernightRatings = SelectOvernightRatings(episode, options),
            CalculatedConsolidatedRatings = SelectConsolidatedRatings(episode, options),
            CalculatedConsolidatedExcessRatings = SelectConsolidatedExcessRatings(episode, options),
            CalculatedExtendedRatings = SelectExtendedRatings(episode, options),
            CalculatedExtendedExcessRatings = SelectExtendedExcessRatings(episode, options)
        };

        return dataPoint;
    }

    private static bool IsFormatIncluded(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.IncludeSpecials || episode.EpisodeFormatId == EpisodeFormats.Series;

    private static decimal? SelectRating(Episode episode, Bottom20EpisodesDataOptions options)
    {
        var result = options.CalculationMethod switch
        {
            Bottom20EpisodesCalculationMethod.Overnight => SelectOvernightRatings(episode, options),
            Bottom20EpisodesCalculationMethod.Consolidated => SelectConsolidatedRatings(episode, options)
                                                              ?? SelectOvernightRatings(episode, options),
            Bottom20EpisodesCalculationMethod.Extended => SelectExtendedRatings(episode, options)
                                                          ?? SelectConsolidatedRatings(episode, options)
                                                          ?? SelectOvernightRatings(episode, options),
            _ => throw new ArgumentOutOfRangeException(nameof(options))
        };

        return result;
    }

    private static decimal? SelectOvernightRatings(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedOvernightRatings : episode.OvernightRatings;

    private static decimal? SelectConsolidatedRatings(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedRatings : episode.ConsolidatedRatings;

    private static decimal? SelectConsolidatedExcessRatings(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedExcessRatings : episode.ConsolidatedExcessRatings;

    private static decimal? SelectExtendedRatings(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedRatings : episode.ExtendedRatings;

    private static decimal? SelectExtendedExcessRatings(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedExcessRatings : episode.ExtendedExcessRatings;
}
