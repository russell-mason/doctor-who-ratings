namespace DoctorWhoRatings.Data.Charting.Bottom20Episodes;

public class Bottom20EpisodesDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IBottom20EpisodesDataPointGenerator
{
    public List<Bottom20EpisodesDataPoint> Generate(Bottom20EpisodesDataOptions options) =>
        dataProvider.DoctorWhoData.Episodes
                    .Where(episode => IsComparableRatingIncluded(episode, options))
                    .Where(episode => IsFormatIncluded(episode, options))
                    .OrderBy(episode => SelectRating(episode, options))
                    .Take(20)
                    .Select(episode => CreateDataPoint(episode, options))
                    .ToList();

    private static Bottom20EpisodesDataPoint CreateDataPoint(Episode episode, Bottom20EpisodesDataOptions options) =>
        new()
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

    private static bool IsComparableRatingIncluded(Episode episode, Bottom20EpisodesDataOptions options) =>
        !options.OnlyIncludeComparableRatings ||
        (options.CalculationMethod == Bottom20EpisodesCalculationMethod.Overnight && episode.OvernightRatings != null) ||
        (options.CalculationMethod == Bottom20EpisodesCalculationMethod.Consolidated && episode.ConsolidatedRatings != null) ||
        (options.CalculationMethod == Bottom20EpisodesCalculationMethod.Extended && episode.ExtendedExcessRatings != null);

    private static bool IsFormatIncluded(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.IncludeSpecials || episode.EpisodeFormatId == EpisodeFormats.Series;

    private static decimal? SelectRating(Episode episode, Bottom20EpisodesDataOptions options) =>
        options.CalculationMethod switch
        {
            Bottom20EpisodesCalculationMethod.Overnight => SelectOvernightRatings(episode, options),

            Bottom20EpisodesCalculationMethod.Consolidated => SelectConsolidatedRatings(episode, options)
                                                              ?? SelectOvernightRatings(episode, options),

            Bottom20EpisodesCalculationMethod.Extended => SelectExtendedRatings(episode, options)
                                                          ?? SelectConsolidatedRatings(episode, options)
                                                          ?? SelectOvernightRatings(episode, options),

            _ => throw new ArgumentOutOfRangeException(nameof(options))
        };

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
