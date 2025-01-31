namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.Bottom20Episodes;

public static class Bottom20EpisodesChartOptions
{
    public static ApexChartOptions<Bottom20EpisodesDataPoint> Defaults =>
        new()
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
                    Text = "Episode",
                    OffsetY = -8
                }
            },
            Yaxis =
            [
                new YAxis
                {
                    Labels = new YAxisLabels
                    {
                        Formatter = "function(value) { return value.toFixed(2); }"
                    },
                    Title = new AxisTitle
                    {
                        Text = "Ratings (millions)"
                    }
                }
            ],
            Chart = new Chart
            {
                Stacked = true
            },
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };
}
