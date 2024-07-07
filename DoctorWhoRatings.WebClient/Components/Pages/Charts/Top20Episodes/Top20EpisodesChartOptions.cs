namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.Top20Episodes;

public static class Top20EpisodesChartOptions
{
    public static ApexChartOptions<Top20EpisodesDataPoint> Defaults =>
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
            },
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };
}
