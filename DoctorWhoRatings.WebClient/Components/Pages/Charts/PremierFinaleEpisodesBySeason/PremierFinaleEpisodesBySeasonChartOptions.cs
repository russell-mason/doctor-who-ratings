namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.PremierFinaleEpisodesBySeason;

public static class PremierFinaleEpisodesBySeasonChartOptions
{
    public static ApexChartOptions<PremierFinaleEpisodeBySeasonDataPoint> Defaults =>
        new()
        {
            Chart = new Chart
            {
                Animations = new Animations
                {
                    AnimateGradually = new AnimateGradually
                    {
                        Delay = 10
                    },
                    Speed = 80
                },
                Toolbar = new Toolbar
                {
                    Tools = new Tools
                    {
                        Reset = false,
                        Zoom = false,
                        Pan = false,
                        Zoomin = false,
                        Zoomout = false
                    }
                }
            },
            PlotOptions = new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    ColumnWidth = "90%"
                }
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
                Width = 450,
                Position = LegendPosition.Right
            }
        };
}
