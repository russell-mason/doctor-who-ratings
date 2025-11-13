namespace DoctorWhoRatings.WebClient.Components.Extensions;

public static class ApexChartExtensions
{
    extension<T>(ApexChart<T> chart) where T : class
    {
        public async Task AddCustomAnnotations(Annotations annotations)
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
}
