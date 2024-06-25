namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public class AverageByDoctorDataGenerator(IDoctorWhoDataProvider dataProvider) : IAverageByDoctorDataGenerator
{
    public AverageByDoctorData Generate(AverageByDoctorDataOptions options)
    {
        var dataPoints = dataProvider.DoctorWhoData.Episodes
                                     .GroupBy(episode => episode.Doctor)
                                     .Select(group =>
                                     {
                                         var overnights = SelectRatings(group, e => e.OvernightRatings);
                                         var consolidated = SelectRatings(group, e => e.ConsolidatedRatings);
                                         var extended = SelectRatings(group, e => e.ExtendedRatings);

                                         var consolidatedExcess = SelectRatings(group,
                                             e => e is { ConsolidatedRatings: not null, OvernightRatings: not null },
                                             e => e.ConsolidatedRatings - e.OvernightRatings);

                                         var extendedExcess = SelectRatings(group,
                                             e => e is { ExtendedRatings: not null, ConsolidatedRatings: not null },
                                             e => e.ExtendedRatings - e.ConsolidatedRatings);

                                         var adjustedOvernights = SelectRatings(group, e => e.PopulationAdjustedOvernightRatings);
                                         var adjustedConsolidated = SelectRatings(group, e => e.PopulationAdjustedConsolidatedRatings);
                                         var adjustedExtended = SelectRatings(group, e => e.PopulationAdjustedExtendedRatings);

                                         var adjustedConsolidatedExcess = SelectRatings(group,
                                             e => e is { PopulationAdjustedConsolidatedRatings: not null, PopulationAdjustedOvernightRatings: not null },
                                             e => e.PopulationAdjustedConsolidatedRatings - e.PopulationAdjustedOvernightRatings);

                                         var adjustedExtendedExcess = SelectRatings(group,
                                             e => e is { PopulationAdjustedExtendedRatings: not null, PopulationAdjustedConsolidatedRatings: not null },
                                             e => e.PopulationAdjustedExtendedRatings - e.PopulationAdjustedConsolidatedRatings);

                                         var dataPoint = new AverageByDoctorDataPoint
                                         {
                                             Doctor = group.Key,
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
                                     })
                                     .ToList()
                                     .AsReadOnly();

        var averageByDoctorData = new AverageByDoctorData()
        {
            DataPoints = dataPoints
        };

        return averageByDoctorData;
    }

    private static List<decimal> SelectRatings(IGrouping<int, Episode> group, Func<Episode, decimal?> selector)
    {
        var result = group.Where(e => selector(e) != null)
             .Select(selector)
             .Cast<decimal>()
             .ToList();

        return result;
    }

    private static List<decimal> SelectRatings(IGrouping<int, Episode> group, Func<Episode, bool> predicate, Func<Episode, decimal?> selector)
    {
        var result = group.Where(predicate)
                          .Select(selector)
                          .Cast<decimal>()
                          .ToList();

        return result;
    }

    private static decimal? AverageOf(List<decimal> values) => values.Count > 0 ? values.Average() : null;

    private static decimal? MedianOf(List<decimal> values) => values.Count > 0 ? values.Median() : null;

    private static decimal? Calculate(List<decimal> values, CalculationMethod calculationMethod) =>
        calculationMethod == CalculationMethod.Mean ? AverageOf(values) : MedianOf(values);
}
