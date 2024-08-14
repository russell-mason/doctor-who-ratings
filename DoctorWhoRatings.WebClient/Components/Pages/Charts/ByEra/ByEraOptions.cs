namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.ByEra;

public static class ByEraChartOptions
{
    public static ApexChartOptions<ByEraDataPoint> Defaults =>
        new()
        {
            DataLabels = new DataLabels
            {
                DropShadow = new DropShadow
                {
                    Enabled = false
                },
                Formatter = "function(value) { return value.toFixed(2) + '%'; }"
            },
            Tooltip = new Tooltip
            {
                FollowCursor = true
            }
        };
}
