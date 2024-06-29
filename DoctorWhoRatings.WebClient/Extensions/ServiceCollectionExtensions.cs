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

        return services;
    }
}
