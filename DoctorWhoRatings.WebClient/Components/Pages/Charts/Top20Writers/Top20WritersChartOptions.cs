namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.Top20Writers;

public static class Top20WritersChartOptions
{
    public static ApexChartOptions<Top20WritersDataPoint> Defaults =>
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
                Title = new AxisTitle
                {
                    Text = "Writer",
                    OffsetY = -8
                }
            },
            Yaxis =
            [
                new YAxis
                {
                    Labels = new YAxisLabels
                    {
                        Formatter = "function(value) { value; }"
                    },
                    Title = new AxisTitle
                    {
                        Text = "Number of episodes"
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
