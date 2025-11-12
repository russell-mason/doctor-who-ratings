namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IAverageByDoctorDataPointGenerator
{
    public List<AverageByDoctorDataPoint> Generate(AverageByDoctorDataOptions options) =>
        dataProvider.DoctorWhoData.Episodes
                    .Where(episode => IsFormatIncluded(episode, options))
                    .GroupBy(episode => episode.Doctor)
                    .Select(group => CreateDataPoint(group, options))
                    .ToList();

    private static bool IsFormatIncluded(Episode episode, AverageByDoctorDataOptions options) =>
        options.IncludeSpecials || episode.EpisodeFormatId != EpisodeFormats.Special;

    private static AverageByDoctorDataPoint CreateDataPoint(IGrouping<int, Episode> group, AverageByDoctorDataOptions options)
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

        var dataPoint = new AverageByDoctorDataPoint
        {
            Actor = group.First().Actor,
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

    private static List<decimal> SelectRatings(IGrouping<int, Episode> group, Func<Episode, decimal?> selector) =>
        group.Where(e => selector(e) != null)
             .Select(selector)
             .Cast<decimal>()
             .ToList();

    private static decimal? Calculate(List<decimal> values, AverageByDoctorCalculationMethod calculationMethod) =>
        calculationMethod == AverageByDoctorCalculationMethod.Mean ? AverageOf(values) : MedianOf(values);

    private static decimal? AverageOf(List<decimal> values) => values.Count > 0 ? values.Average() : null;

    private static decimal? MedianOf(List<decimal> values) => values.Count > 0 ? values.Median() : null;
}
