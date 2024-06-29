namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public class HighLowEpisodesByDoctorDataOptions
{
    public bool AdjustForCurrentPopulation { get; set; } = false;

    public HighLowEpisodesCalculationMethod CalculationMethod { get; set; } = HighLowEpisodesCalculationMethod.Overnight;
}