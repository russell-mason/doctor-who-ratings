namespace DoctorWhoRatings.WebClient.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDoctorWhoCharting()
        {
            services.AddSingleton<IExcelSpreadsheetReader, ExcelSpreadsheetReader>();
            services.AddSingleton<IDoctorWhoDataReader, DoctorWhoDataReader>();
            services.AddSingleton<IDoctorWhoDataProvider, DoctorWhoDataProvider>();
            services.AddSingleton<IDashboardGenerator, DashboardGenerator>();
            services.AddSingleton<IEpisodesDataPointGenerator, EpisodesDataPointGenerator>();
            services.AddSingleton<IAverageByDoctorDataPointGenerator, AverageByDoctorDataPointGenerator>();
            services.AddSingleton<IAverageBySeasonDataPointGenerator, AverageBySeasonDataPointGenerator>();
            services.AddSingleton<IAverageByStoryDataPointGenerator, AverageByStoryDataPointGenerator>();
            services.AddSingleton<IHighLowEpisodesByDoctorDataPointGenerator, HighLowEpisodesByDoctorDataPointGenerator>();
            services.AddSingleton<IPremierFinaleEpisodesBySeasonDataPointGenerator, PremierFinaleEpisodesBySeasonDataPointGenerator>();
            services.AddSingleton<IPopulationByYearDataPointGenerator, PopulationByYearDataPointGenerator>();
            services.AddSingleton<ITop20EpisodesDataPointGenerator, Top20EpisodesDataPointGenerator>();
            services.AddSingleton<IBottom20EpisodesDataPointGenerator, Bottom20EpisodesDataPointGenerator>();
            services.AddSingleton<IShareByHoursWatchedDataPointGenerator, ShareByHoursWatchedDataPointGenerator>();
            services.AddSingleton<IShareByEraDataPointGenerator, ShareByEraDataPointGenerator>();
            services.AddSingleton<ITimelineDataPointGenerator, TimelineDataPointGenerator>();
            services.AddSingleton<IEpisodeGuideDataPointGenerator, EpisodeGuideDataPointGenerator>();
            services.AddSingleton<IEpisodeInContextDataPointGenerator, EpisodeInContextDataPointGenerator>();
            services.AddSingleton<ITrendDataPointGenerator, TrendDataPointGenerator>();
            services.AddSingleton<IShareByContentDataPointGenerator, ShareByContentDataPointGenerator>();
            services.AddSingleton<IShareByMissingEpisodesDataPointGenerator, ShareByMissingEpisodesDataPointGenerator>();
            services.AddSingleton<ITop20WritersDataPointGenerator, Top20WritersDataPointGenerator>();

            services.AddScoped<IEpisodeContextDrillDown, EpisodeContextDrillDown>();

            return services;
        }
    }
}
