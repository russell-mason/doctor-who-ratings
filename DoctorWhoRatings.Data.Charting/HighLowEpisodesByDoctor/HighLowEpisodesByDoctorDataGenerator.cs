namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public class HighLowEpisodesByDoctorDataGenerator(IDoctorWhoDataProvider dataProvider) : IHighLowEpisodesByDoctorDataGenerator
{
    public HighLowEpisodesByDoctorData Generate(HighLowEpisodesByDoctorDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.Doctor)
                                     .Select(group => CreateDataPoint(group, options))
                                     .ToList()
                                     .AsReadOnly();

        var highLowEpisodesByDoctorData = new HighLowEpisodesByDoctorData()
        {
            DataPoints = dataPoints
        };

        return highLowEpisodesByDoctorData;
    }

    private static HighLowEpisodesByDoctorDataPoint CreateDataPoint(IGrouping<int, Episode> group,
                                                                    HighLowEpisodesByDoctorDataOptions options)
    {
        var highEpisode = SelectEpisode(group, options, true);
        var lowEpisode = SelectEpisode(group, options, false);

        var dataPoint = CreateDataPoint(highEpisode, lowEpisode, group.Count(), options);

        return dataPoint;
    }

    private static HighLowEpisodesByDoctorDataPoint CreateDataPoint(Episode highEpisode,
                                                                    Episode lowEpisode,
                                                                    int episodeCount,
                                                                    HighLowEpisodesByDoctorDataOptions options)
    {
        var dataPoint = new HighLowEpisodesByDoctorDataPoint
        {
            Actor = highEpisode.Actor,
            EpisodeCount = episodeCount,

            HighEpisodeDataPoint = new HighLowEpisodeByDoctorDataPoint
            {
                StoryTitle = highEpisode.StoryTitle,
                StoryPartTitle = highEpisode.PartTitle,
                OvernightRatings = highEpisode.OvernightRatings,
                ConsolidatedRatings = highEpisode.ConsolidatedRatings,
                ConsolidatedExcessRatings = highEpisode.ConsolidatedExcessRatings,
                ExtendedRatings = highEpisode.ExtendedRatings,
                ExtendedExcessRatings = highEpisode.ExtendedExcessRatings,
                PopulationAdjustedOvernightRatings = highEpisode.PopulationAdjustedOvernightRatings,
                PopulationAdjustedConsolidatedRatings = highEpisode.PopulationAdjustedConsolidatedRatings,
                PopulationAdjustedConsolidatedExcessRatings = highEpisode.PopulationAdjustedConsolidatedExcessRatings,
                PopulationAdjustedExtendedRatings = highEpisode.PopulationAdjustedExtendedRatings,
                PopulationAdjustedExtendedExcessRatings = highEpisode.PopulationAdjustedExtendedExcessRatings,
                CalculatedOvernightRatings = SelectOvernightRatings(highEpisode, options),
                CalculatedConsolidatedRatings = SelectConsolidatedRatings(highEpisode, options),
                CalculatedConsolidatedExcessRatings = SelectConsolidatedExcessRatings(highEpisode, options),
                CalculatedExtendedRatings = SelectExtendedRatings(highEpisode, options),
                CalculatedExtendedExcessRatings = SelectExtendedExcessRatings(highEpisode, options),
            },
            LowEpisodeDataPoint = new HighLowEpisodeByDoctorDataPoint
            {
                StoryTitle = lowEpisode.StoryTitle,
                StoryPartTitle = lowEpisode.PartTitle,
                OvernightRatings = lowEpisode.OvernightRatings,
                ConsolidatedRatings = lowEpisode.ConsolidatedRatings,
                ConsolidatedExcessRatings = lowEpisode.ConsolidatedExcessRatings,
                ExtendedRatings = lowEpisode.ExtendedRatings,
                ExtendedExcessRatings = lowEpisode.ExtendedExcessRatings,
                PopulationAdjustedOvernightRatings = lowEpisode.PopulationAdjustedOvernightRatings,
                PopulationAdjustedConsolidatedRatings = lowEpisode.PopulationAdjustedConsolidatedRatings,
                PopulationAdjustedConsolidatedExcessRatings = lowEpisode.PopulationAdjustedConsolidatedExcessRatings,
                PopulationAdjustedExtendedRatings = lowEpisode.PopulationAdjustedExtendedRatings,
                PopulationAdjustedExtendedExcessRatings = lowEpisode.PopulationAdjustedExtendedExcessRatings,
                CalculatedOvernightRatings = SelectOvernightRatings(lowEpisode, options),
                CalculatedConsolidatedRatings = SelectConsolidatedRatings(lowEpisode, options),
                CalculatedConsolidatedExcessRatings = SelectConsolidatedExcessRatings(lowEpisode, options),
                CalculatedExtendedRatings = SelectExtendedRatings(lowEpisode, options),
                CalculatedExtendedExcessRatings = SelectExtendedExcessRatings(lowEpisode, options)
            }
        };

        return dataPoint;
    }

    private static Episode SelectEpisode(IGrouping<int, Episode> group, HighLowEpisodesByDoctorDataOptions options, bool highest) =>
        options.CalculationMethod switch
        {
            HighLowEpisodesCalculationMethod.Overnight => SelectEpisodeByOvernight(group, options, highest),
            HighLowEpisodesCalculationMethod.Consolidated => SelectEpisodeByConsolidated(group, options, highest),
            HighLowEpisodesCalculationMethod.Extended => SelectEpisodeByExtended(group, options, highest),
            _ => throw new InvalidOperationException(nameof(options.CalculationMethod))
        };

    private static Episode SelectEpisode(IGrouping<int, Episode> group, Func<Episode, decimal?> selector, bool descending)
    {
        var nonNulls = group.Where(e => selector(e) != null);

        var orderedEpisodes = descending
            ? nonNulls.OrderByDescending(selector)
            : nonNulls.OrderBy(selector);

        var result = orderedEpisodes.First();
        return result;
    }

    private static Episode SelectEpisodeByOvernight(IGrouping<int, Episode> group, HighLowEpisodesByDoctorDataOptions options, bool descending) =>
        SelectEpisode(group,
            episode => options.AdjustForCurrentPopulation
                ? episode.PopulationAdjustedOvernightRatings
                : episode.OvernightRatings, descending);

    private static Episode SelectEpisodeByConsolidated(IGrouping<int, Episode> group, HighLowEpisodesByDoctorDataOptions options, bool descending) =>
        SelectEpisode(group,
            episode => options.AdjustForCurrentPopulation
                ? episode.PopulationAdjustedConsolidatedExcessRatings ?? episode.PopulationAdjustedOvernightRatings
                : episode.ConsolidatedRatings ?? episode.OvernightRatings,
            descending);

    private static Episode SelectEpisodeByExtended(IGrouping<int, Episode> group, HighLowEpisodesByDoctorDataOptions options, bool descending) =>
        SelectEpisode(group,
            episode => options.AdjustForCurrentPopulation
                ? episode.PopulationAdjustedExtendedRatings ?? episode.PopulationAdjustedConsolidatedRatings ?? episode.PopulationAdjustedOvernightRatings
                : episode.ExtendedRatings ?? episode.ConsolidatedRatings ?? episode.OvernightRatings,
            descending);

    private static decimal? SelectOvernightRatings(Episode episode, HighLowEpisodesByDoctorDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedOvernightRatings : episode.OvernightRatings;

    private static decimal? SelectConsolidatedRatings(Episode episode, HighLowEpisodesByDoctorDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedRatings : episode.ConsolidatedRatings;

    private static decimal? SelectConsolidatedExcessRatings(Episode episode, HighLowEpisodesByDoctorDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedExcessRatings : episode.ConsolidatedExcessRatings;

    private static decimal? SelectExtendedRatings(Episode episode, HighLowEpisodesByDoctorDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedRatings : episode.ExtendedRatings;

    private static decimal? SelectExtendedExcessRatings(Episode episode, HighLowEpisodesByDoctorDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedExcessRatings : episode.ExtendedExcessRatings;
}
