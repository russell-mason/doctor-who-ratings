namespace DoctorWhoRatings.Data.Charting.AverageBySeason;

public class AverageBySeasonDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IAverageBySeasonDataPointGenerator
{
    public List<AverageBySeasonDataPoint> Generate(AverageBySeasonDataOptions options) =>
        dataProvider.DoctorWhoData.Episodes
                    .Where(episode => episode.Season != null)
                    .GroupBy(episode => (episode.Doctor, episode.Season!.Value))
                    .Select((group, index) => CreateDataPoint(index, group, options))
                    .ToList();

    private static AverageBySeasonDataPoint CreateDataPoint(int index, 
                                                            IGrouping<(int, int), Episode> group, 
                                                            AverageBySeasonDataOptions options)
    {
        var overnights = SelectRatings(group, episode => episode.OvernightRatings);
        var consolidated = SelectRatings(group, episode => episode.ConsolidatedRatings);
        var extended = SelectRatings(group, episode => episode.ExtendedRatings);
        var consolidatedExcess = SelectRatings(group, episode => episode.ConsolidatedExcessRatings);
        var extendedExcess = SelectRatings(group, episode => episode.ExtendedExcessRatings);

        var adjustedOvernights = SelectRatings(group, episode => episode.PopulationAdjustedOvernightRatings);
        var adjustedConsolidated = SelectRatings(group, episode => episode.PopulationAdjustedConsolidatedRatings);
        var adjustedExtended = SelectRatings(group, episode => episode.PopulationAdjustedExtendedRatings);
        var adjustedConsolidatedExcess = SelectRatings(group, episode => episode.PopulationAdjustedConsolidatedExcessRatings);
        var adjustedExtendedExcess = SelectRatings(group, episode => episode.PopulationAdjustedExtendedExcessRatings);

        var dataPoint = new AverageBySeasonDataPoint
        {
            Id = index + 1,
            Actor = group.First().Actor,
            Season = group.First().Season!.Value,
            SeasonFormatDescription = group.First().SeasonFormatDescription,
            OriginalAirDate = group.First().OriginalAirDate,
            EpisodeCount = group.Count(),
            CalculatedOvernightRatings = Calculate(overnights, options.CalculationMethod),
            CalculatedConsolidatedRatings = Calculate(consolidated, options.CalculationMethod),
            CalculatedExtendedRatings = Calculate(extended, options.CalculationMethod),
            CalculatedConsolidatedExcessRatings = Calculate(consolidatedExcess, options.CalculationMethod),
            CalculatedExtendedExcessRatings = Calculate(extendedExcess, options.CalculationMethod),
            CalculatedPopulationAdjustedOvernightRatings = Calculate(adjustedOvernights, options.CalculationMethod),
            CalculatedPopulationAdjustedConsolidatedRatings = Calculate(adjustedConsolidated, options.CalculationMethod),
            CalculatedPopulationAdjustedExtendedRatings = Calculate(adjustedExtended, options.CalculationMethod),
            CalculatedPopulationAdjustedConsolidatedExcessRatings = Calculate(adjustedConsolidatedExcess, options.CalculationMethod),
            CalculatedPopulationAdjustedExtendedExcessRatings = Calculate(adjustedExtendedExcess, options.CalculationMethod),
        };

        return dataPoint;
    }

    private static List<decimal> SelectRatings(IGrouping<(int, int), Episode> group, Func<Episode, decimal?> selector) =>
        group.Where(episode => selector(episode) != null)
             .Select(selector)
             .Cast<decimal>()
             .ToList();

    private static decimal? Calculate(List<decimal> values, AverageBySeasonCalculationMethod calculationMethod) =>
        calculationMethod == AverageBySeasonCalculationMethod.Mean ? AverageOf(values) : MedianOf(values);

    private static decimal? AverageOf(List<decimal> values) => values.Count > 0 ? values.Average() : null;

    private static decimal? MedianOf(List<decimal> values) => values.Count > 0 ? values.Median() : null;
}
