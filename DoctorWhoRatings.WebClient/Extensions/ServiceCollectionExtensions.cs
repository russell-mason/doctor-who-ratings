namespace DoctorWhoRatings.WebClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDoctorWhoCharting(this IServiceCollection services)
    {
        services.AddSingleton<IExcelSpreadsheetReader, ExcelSpreadsheetReader>();
        services.AddSingleton<IDoctorWhoDataReader, DoctorWhoDataReader>();
        services.AddSingleton<IDoctorWhoDataProvider, DoctorWhoDataProvider>();
        services.AddSingleton<IAllEpisodesDataGenerator, AllEpisodesDataGenerator>();
        services.AddSingleton<IAverageByDoctorDataGenerator, AverageByDoctorDataGenerator>();
        services.AddSingleton<IHighLowEpisodesByDoctorDataGenerator, HighLowEpisodesByDoctorDataGenerator>();
        services.AddSingleton<IPopulationByYearDataGenerator, PopulationByYearDataGenerator>();
        services.AddSingleton<ITop20EpisodesDataGenerator, Top20EpisodesDataGenerator>();
        services.AddSingleton<IBottom20EpisodesDataGenerator, Bottom20EpisodesDataGenerator>();

        return services;
    }
}
