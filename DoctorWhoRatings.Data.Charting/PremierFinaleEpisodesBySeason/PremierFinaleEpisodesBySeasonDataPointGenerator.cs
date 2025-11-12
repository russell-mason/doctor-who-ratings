namespace DoctorWhoRatings.Data.Charting.PremierFinaleEpisodesBySeason;

public class PremierFinaleEpisodesBySeasonDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IPremierFinaleEpisodesBySeasonDataPointGenerator
{
    public Dictionary<string, List<PremierFinaleEpisodeBySeasonDataPoint>> Generate(PremierFinaleEpisodesBySeasonDataOptions options) =>
        dataProvider.DoctorWhoData.Episodes
                    .Where(episode => episode.Season.HasValue)
                    .GroupBy(episode => episode.SeasonDescription)
                    .Select(group => CreateDataPoints(group, options))
                    .ToDictionary();

    private static (string, List<PremierFinaleEpisodeBySeasonDataPoint>) CreateDataPoints(IGrouping<string, Episode> group,
                                                                                          PremierFinaleEpisodesBySeasonDataOptions options)
    {
        var premierDataPoint = CreateDataPoint(true, group.First(), group.Count(), options);
        var finaleDataPoint = CreateDataPoint(false, group.Last(), group.Count(), options);

        List<PremierFinaleEpisodeBySeasonDataPoint> dataPoints = [premierDataPoint, finaleDataPoint];

        return (premierDataPoint.SeasonDescription, dataPoints);
    }

    private static PremierFinaleEpisodeBySeasonDataPoint CreateDataPoint(bool isPremier,
                                                                         Episode episode,
                                                                         int episodeCount,
                                                                         PremierFinaleEpisodesBySeasonDataOptions options) =>
        new()
        {
            IsPremier = isPremier,
            EpisodePosition = isPremier ? "Premier" : "Finale",
            EpisodeCount = episodeCount,
            Actor = episode.Actor,
            Season = episode.Season,
            SeasonFormatDescription = episode.SeasonFormatDescription,
            SeasonDescription = episode.SeasonDescription,
            StoryTitle = episode.StoryTitle,
            StoryPartTitle = episode.PartTitle,
            Slug = episode.Slug,
            OvernightRatings = episode.OvernightRatings,
            ConsolidatedRatings = episode.ConsolidatedRatings,
            ConsolidatedExcessRatings = episode.ConsolidatedExcessRatings,
            ExtendedRatings = episode.ExtendedRatings,
            ExtendedExcessRatings = episode.ExtendedExcessRatings,
            PopulationAdjustedOvernightRatings = episode.PopulationAdjustedOvernightRatings,
            PopulationAdjustedConsolidatedRatings = episode.PopulationAdjustedConsolidatedRatings,
            PopulationAdjustedConsolidatedExcessRatings = episode.PopulationAdjustedConsolidatedExcessRatings,
            PopulationAdjustedExtendedRatings = episode.PopulationAdjustedExtendedRatings,
            PopulationAdjustedExtendedExcessRatings = episode.PopulationAdjustedExtendedExcessRatings,
            CalculatedRatings = SelectRatings(episode, options)
        };

    private static decimal? SelectRatings(Episode episode, PremierFinaleEpisodesBySeasonDataOptions options)
    {
        var overnightRatings = SelectOvernightRatings(episode, options);

        if (options.CalculationMethod == PremierFinaleEpisodesBySeasonCalculationMethod.Overnight)
        {
            return overnightRatings;
        }

        var consolidatedRatings = SelectConsolidatedRatings(episode, options);

        if (options.CalculationMethod == PremierFinaleEpisodesBySeasonCalculationMethod.Consolidated)
        {
            return consolidatedRatings ?? overnightRatings;
        }

        var extendedRatings = SelectExtendedRatings(episode, options);

        return extendedRatings ?? consolidatedRatings ?? overnightRatings;
    }

    private static decimal? SelectOvernightRatings(Episode episode, PremierFinaleEpisodesBySeasonDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedOvernightRatings : episode.OvernightRatings;

    private static decimal? SelectConsolidatedRatings(Episode episode, PremierFinaleEpisodesBySeasonDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedConsolidatedRatings : episode.ConsolidatedRatings;

    private static decimal? SelectExtendedRatings(Episode episode, PremierFinaleEpisodesBySeasonDataOptions options) =>
        options.AdjustForCurrentPopulation ? episode.PopulationAdjustedExtendedRatings : episode.ExtendedRatings;
}
