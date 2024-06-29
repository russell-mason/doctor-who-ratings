namespace DoctorWhoRatings.Data.Charting.HighLowEpisodesByDoctor;

public interface IHighLowEpisodesByDoctorDataGenerator
{
    HighLowEpisodesByDoctorData Generate(HighLowEpisodesByDoctorDataOptions options);
}