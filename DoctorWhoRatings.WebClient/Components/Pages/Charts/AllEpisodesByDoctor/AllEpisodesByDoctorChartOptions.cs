namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.AllEpisodesByDoctor;

public static class AllEpisodesByDoctorChartOptions
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
                TickPlacement = TickPlacement.On
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

    public static Annotations CreateAnnotations(List<EpisodeDataPoint> dataPoints)
    {
        var xAxis = new List<(int id, string season)>();
        
        // Can't be grouped because the same actor may have multiple specials at different points and 
        // Specials don't have a season

        for (var index = 0 ; index < dataPoints.Count; index++)
        {
            // Look back and ignore if it's the same as the last
            // Look forward only to determine if "Special" should be pluralised

            var currentDataPoint = dataPoints[index];
            var lastDataPoint = index - 1 < 0 ? null : dataPoints[index - 1];
            var nextDataPoint = index + 1 >= dataPoints.Count ? null : dataPoints[index + 1];

            var isSameAsLast = currentDataPoint.SeasonFormatDescription == lastDataPoint?.SeasonFormatDescription &&
                               currentDataPoint.Season == lastDataPoint.Season;

            if (isSameAsLast)
            {
                continue;
            }

            var isSameAsNext = currentDataPoint.SeasonFormatDescription == nextDataPoint?.SeasonFormatDescription &&
                               currentDataPoint.Season == nextDataPoint.Season;

            var season = currentDataPoint.Season == null ? isSameAsNext ? "s" : string.Empty : $" {currentDataPoint.Season}";
            season = currentDataPoint.SeasonFormatDescription + season;

            xAxis.Add((id: currentDataPoint.Id, season));
        }

        var annotations = new Annotations
        {
            Xaxis = ApexChartOptionsExtensions.CreateAnnotationsXAxis(xAxis)
        };

        return annotations;
    }
}
