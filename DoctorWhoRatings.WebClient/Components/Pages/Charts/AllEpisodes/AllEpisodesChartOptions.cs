namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodes;

public static class AllEpisodesChartOptions
{
    public static ApexChartOptions<EpisodeDataPoint> Defaults =>
        new()
        {
            Chart = new Chart 
            { 
                Id = "all-episodes" 
            },
            Markers = new Markers
            {
                Size = 2,
                StrokeWidth = 0,
                Colors = "#1E5EAB"
            },
            Stroke = new Stroke
            {
                Width = 2
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
                Tooltip = new XAxisTooltip { Enabled = false },
                Crosshairs = new AxisCrosshairs { Show = false }
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
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };
}
