namespace DoctorWhoRatings.Data.Charting.AverageByDoctor;

public interface IAverageByDoctorDataGenerator
{
    AverageByDoctorData Generate(AverageByDoctorDataOptions options);
}