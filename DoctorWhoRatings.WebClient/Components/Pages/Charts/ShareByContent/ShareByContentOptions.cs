namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.ShareByContent;

public static class ShareByContentChartOptions
{
    public static ApexChartOptions<ShareByContentDataPoint> Defaults =>
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
