namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.Population;

public static class PopulationByYearChartOptions
{
    public static ApexChartOptions<PopulationByYearDataPoint> Defaults =>
        new()
        {
            Markers = new Markers
            {
                Size = 3
            },
            Stroke = new Stroke
            {
                Curve = new CurveSelections(Curve.MonotoneCubic)
            },
            Xaxis = new XAxis
            {
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
                    Labels = new YAxisLabels
                    {
                        Formatter = "function(value) { return Math.round(value / 1000000, 0); }"
                    }
                }
            ],
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };
}
