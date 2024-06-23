namespace DoctorWhoRatings.WebClient.Components.Pages.Charts;

public static class ChartOptionDefaults
{
    public static ApexChartOptions<T> ForBarChart<T>(string xAxisTitle) where T : class
    {
        ApexChartOptions<T> chartOptions = new()
        {
            PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    ColumnWidth = "90%"
                }
            },
            Xaxis = new XAxis
            {
                Title = new AxisTitle
                {
                    Text = xAxisTitle,
                    OffsetY = -6
                },
                Labels = new XAxisLabels
                {
                    Show = false
                },
                TickPlacement = TickPlacement.On
            }
        };

        return chartOptions;
    }
}
