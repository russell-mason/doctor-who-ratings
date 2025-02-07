namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.ShareByEra;

public static class ShareByEraChartOptions
{
    public static ApexChartOptions<ShareByEraDataPoint> Defaults =>
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
