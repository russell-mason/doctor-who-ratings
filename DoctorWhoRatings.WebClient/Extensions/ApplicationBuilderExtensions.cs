namespace DoctorWhoRatings.WebClient.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDoctorWhoCharting(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IDoctorWhoDataProvider>();

        provider.Load();

        return app;
    }
}
