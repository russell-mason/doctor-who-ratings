namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodes;

public static class AllEpisodesChartOptions
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
                    }
                }
            ],
            Tooltip = new Tooltip
            {
                FollowCursor = true
            },
            Legend = new Legend
            {
                Show = false
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
