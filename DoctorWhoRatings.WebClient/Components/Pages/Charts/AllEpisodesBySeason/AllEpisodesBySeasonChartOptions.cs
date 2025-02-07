namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodesBySeason;

public static class AllEpisodesBySeasonChartOptions
{
    public static ApexChartOptions<EpisodeDataPoint> Defaults =>
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
                Labels = new XAxisLabels
                {
                    Formatter = "function(value) { return '' }",
                    Style = new AxisLabelStyle
                    {
                        FontSize = "0"
                    }
                },
                TickPlacement = TickPlacement.On,
                Title = new AxisTitle
                {
                    Text = "Episode",
                    OffsetY = 8
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
