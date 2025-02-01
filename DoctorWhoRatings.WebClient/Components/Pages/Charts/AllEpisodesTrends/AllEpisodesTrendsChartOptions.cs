namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodesTrends;

public static class AllEpisodesTrendsChartOptions
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
                },
                Title = new AxisTitle
                {
                    Text = "Trend",
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
                        Text = "Ratings (millions)"
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

    public static Annotations CreateAnnotations(List<EpisodeDataPoint> dataPoints, TrendRange range)
    {
        return range switch
        {
            TrendRange.Season => AllEpisodesTrendsChartOptions.CreateSeasonAnnotations(dataPoints),
            TrendRange.Doctor => AllEpisodesTrendsChartOptions.CreateDoctorAnnotations(dataPoints),
            TrendRange.Era => AllEpisodesTrendsChartOptions.CreateEraAnnotations(dataPoints),
            _ => throw new NotSupportedException("Range not supported")
        };
    }

    private static Annotations CreateSeasonAnnotations(List<EpisodeDataPoint> dataPoints)
    {
        var counter = 1;

        var annotations = dataPoints
                          .Where(x => x.Season != null)
                          .GroupBy(dataPoint => new { dataPoint.Actor, dataPoint.SeasonDescription })
                          .Select(group => (index: counter++, text: group.Key.SeasonDescription))
                          .ToList();

        return CreateAnnotations(annotations);
    }

    private static Annotations CreateDoctorAnnotations(List<EpisodeDataPoint> dataPoints)
    {
        var counter = 1;

        var annotations = dataPoints
                          .GroupBy(dataPoint => new { dataPoint.Actor, dataPoint.Doctor })
                          .Select(group => (index: counter++, text: group.Key.Actor))
                          .ToList();

        return CreateAnnotations(annotations);
    }

    private static Annotations CreateEraAnnotations(List<EpisodeDataPoint> dataPoints)
    {
        var counter = 1;

        var annotations = dataPoints
                          .GroupBy(dataPoint => dataPoint.EraDescription)
                          .Select(group => (index: counter++, text: group.Key ))
                          .ToList();

        return CreateAnnotations(annotations);
    }

    private static Annotations CreateAnnotations(List<(int index, string text)> items)
    {
        var annotations = new Annotations
        {
            Xaxis = ApexChartOptionsExtensions.CreateAnnotationsXAxis(items)
        };

        return annotations;
    }
}
