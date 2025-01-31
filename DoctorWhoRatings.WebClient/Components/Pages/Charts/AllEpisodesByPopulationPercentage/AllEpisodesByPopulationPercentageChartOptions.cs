namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodesByPopulationPercentage;

public static class AllEpisodesByPopulationPercentageChartOptions
{
    public static ApexChartOptions<EpisodeDataPoint> Defaults =>
        new()
        {
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
                Tooltip = new XAxisTooltip
                {
                    Enabled = false
                },
                Crosshairs = new AxisCrosshairs
                {
                    Show = false
                },
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
                    Min = 0,
                    Labels = new YAxisLabels
                    {
                        Formatter = "function(value) { return value.toFixed(2); }"
                    },
                    Title = new AxisTitle
                    {
                        Text = "% of Population"
                    }
                }
            ],
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };

    public static Annotations CreateAnnotations(List<EpisodeDataPoint> dataPoints)
    {
        var xAxis = dataPoints
                    .GroupBy(dataPoint => new { dataPoint.Actor, dataPoint.Doctor })
                    .Select(group => (group.First().Id, group.Key.Actor));

        var annotations = new Annotations
        {
            Xaxis = ApexChartOptionsExtensions.CreateAnnotationsXAxis(xAxis)
        };

        return annotations;
    }
}
