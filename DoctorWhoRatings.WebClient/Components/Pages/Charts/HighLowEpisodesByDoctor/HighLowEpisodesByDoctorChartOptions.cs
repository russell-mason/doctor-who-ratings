namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.HighLowEpisodesByDoctor;

public static class HighLowEpisodesByDoctorChartOptions
{
    public static ApexChartOptions<HighLowEpisodesByDoctorDataPoint> Defaults =>
        new()
        {
            PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    ColumnWidth = "90%"
                }
            },
            Yaxis =
            [
                new YAxis
                {
                    Labels = new YAxisLabels
                    {
                        Formatter = "function(value) { return value.toFixed(2); }"
                    }
                }
            ],
            Chart =
            {
                Stacked = true
            }
        };
}
