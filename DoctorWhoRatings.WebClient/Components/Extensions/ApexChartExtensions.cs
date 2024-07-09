namespace DoctorWhoRatings.WebClient.Components.Extensions;

public static class ApexChartExtensions
{
    public static async Task AddCustomAnnotations<T>(this ApexChart<T> chart, Annotations annotations) where T : class
    {
        if (chart.Options.Annotations == null)
        {
            chart.Options.Annotations = annotations;

            return;
        }

        await chart.ClearAnnotationsAsync();

        if (annotations.Xaxis != null)
        {
            foreach (var xAxis in annotations.Xaxis)
            {
                await chart.AddXAxisAnnotationAsync(xAxis, true);
            }
        }
    }
}
