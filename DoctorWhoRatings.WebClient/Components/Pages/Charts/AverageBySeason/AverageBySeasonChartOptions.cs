namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AverageBySeason;

public static class AverageBySeasonChartOptions
{
    public static ApexChartOptions<AverageBySeasonDataPoint> Defaults =>
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
                    Text = "Season",
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

    public static Annotations CreateAnnotations(List<AverageBySeasonDataPoint> dataPoints)
    {
        var xAxis = dataPoints.GroupBy(dataPoint => new { dataPoint.Actor })
                              .Select(group => (group.First().Id, group.First().Actor));

        var annotations = new Annotations
        {
            Xaxis = ApexChartOptionsExtensions.CreateAnnotationsXAxis(xAxis)
        };

        return annotations;
    }
}