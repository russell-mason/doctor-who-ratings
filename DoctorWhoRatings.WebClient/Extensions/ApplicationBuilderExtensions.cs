namespace DoctorWhoRatings.WebClient.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDoctorWhoCharting(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IDoctorWhoDataProvider>();

        // Load the dataset in the background to prevent blocking and causing a timeout when the site starts
        // up. Because the dataset is lazy loaded via Lazy<T> which is thread safe by default, if it's not
        // loaded by the time the first access is made, the request will block at that point
        Task.Run(() =>
        {
            provider.Load();
            provider.Save();
        });

        return app;
    }
}
