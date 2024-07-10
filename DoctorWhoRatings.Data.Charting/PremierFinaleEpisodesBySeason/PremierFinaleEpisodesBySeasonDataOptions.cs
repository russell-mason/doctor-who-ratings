namespace DoctorWhoRatings.Data.Charting.PremierFinaleEpisodesBySeason;

public class PremierFinaleEpisodesBySeasonDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public PremierFinaleEpisodesBySeasonCalculationMethod CalculationMethod { get; set; } = PremierFinaleEpisodesBySeasonCalculationMethod.Overnight;
}