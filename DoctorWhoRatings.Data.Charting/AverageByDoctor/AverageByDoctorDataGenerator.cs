namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataPointGenerator(IDoctorWhoDataProvider dataProvider) : IAverageByDoctorDataPointGenerator
{
    public List<AverageByDoctorDataPoint> Generate(AverageByDoctorDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .Where(episode => IsFormatIncluded(episode, options))
                                     .GroupBy(episode => episode.Doctor)
                                     .Select(group => CreateDataPoint(group, options))
                                     .ToList();

        return dataPoints;
    }

    private static bool IsFormatIncluded(Episode episode, AverageByDoctorDataOptions options) =>
        options.IncludeSpecials || episode.EpisodeFormatId != EpisodeFormats.Special;

    private static AverageByDoctorDataPoint CreateDataPoint(IGrouping<int, Episode> group, AverageByDoctorDataOptions options)
    {
        var overnights = SelectRatings(group, e => e.OvernightRatings);
        var consolidated = SelectRatings(group, e => e.ConsolidatedRatings);
        var extended = SelectRatings(group, e => e.ExtendedRatings);
        var consolidatedExcess = SelectRatings(group, e => e.ConsolidatedExcessRatings);
        var extendedExcess = SelectRatings(group, e => e.ExtendedExcessRatings);

        var adjustedOvernights = SelectRatings(group, e => e.PopulationAdjustedOvernightRatings);
        var adjustedConsolidated = SelectRatings(group, e => e.PopulationAdjustedConsolidatedRatings);
        var adjustedExtended = SelectRatings(group, e => e.PopulationAdjustedExtendedRatings);
        var adjustedConsolidatedExcess = SelectRatings(group, e => e.PopulationAdjustedConsolidatedExcessRatings);
        var adjustedExtendedExcess = SelectRatings(group, e => e.PopulationAdjustedExtendedExcessRatings);

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

    private static List<decimal> SelectRatings(IGrouping<int, Episode> group, Func<Episode, decimal?> selector)
    {
        var result = group.Where(e => selector(e) != null)
             .Select(selector)
             .Cast<decimal>()
             .ToList();

        return result;
    }

    private static decimal? Calculate(List<decimal> values, AverageByDoctorCalculationMethod calculationMethod) =>
        calculationMethod == AverageByDoctorCalculationMethod.Mean ? AverageOf(values) : MedianOf(values);

    private static decimal? AverageOf(List<decimal> values) => values.Count > 0 ? values.Average() : null;

    private static decimal? MedianOf(List<decimal> values) => values.Count > 0 ? values.Median() : null;
}
