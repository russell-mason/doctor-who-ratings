namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AverageByStory;

public static class AverageByStoryChartOptions
{
    public static ApexChartOptions<AverageByStoryDataPoint> Defaults =>
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
                    Text = "Story",
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
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };

    public static Annotations CreateAnnotations(List<AverageByStoryDataPoint> dataPoints)
    {
        var xAxis = dataPoints
                    .GroupBy(dataPoint => new { dataPoint.Actor })
                    .Select(group => (group.First().Id, group.First().Actor));

        var annotations = new Annotations
        {
            Xaxis = ApexChartOptionsExtensions.CreateAnnotationsXAxis(xAxis)
        };

        return annotations;
    }
}