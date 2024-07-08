namespace DoctorWhoRatings.WebClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDoctorWhoCharting(this IServiceCollection services)
    {
        services.AddSingleton<IExcelSpreadsheetReader, ExcelSpreadsheetReader>();
        services.AddSingleton<IDoctorWhoDataReader, DoctorWhoDataReader>();
        services.AddSingleton<IDoctorWhoDataProvider, DoctorWhoDataProvider>();
        services.AddSingleton<IEpisodesDataPointGenerator, EpisodesDataPointGenerator>();
        services.AddSingleton<IAverageByDoctorDataPointGenerator, AverageByDoctorDataPointGenerator>();
        services.AddSingleton<IHighLowEpisodesByDoctorDataPointGenerator, HighLowEpisodesByDoctorDataPointGenerator>();
        services.AddSingleton<IPopulationByYearDataPointGenerator, PopulationByYearDataPointGenerator>();
        services.AddSingleton<ITop20EpisodesDataPointGenerator, Top20EpisodesDataPointGenerator>();
        services.AddSingleton<IBottom20EpisodesDataPointGenerator, Bottom20EpisodesDataPointGenerator>();
        services.AddSingleton<ITotalHoursWatchedByDoctorDataPointGenerator, TotalHoursWatchedByDoctorDataPointGenerator>();

        return services;
    }
}
