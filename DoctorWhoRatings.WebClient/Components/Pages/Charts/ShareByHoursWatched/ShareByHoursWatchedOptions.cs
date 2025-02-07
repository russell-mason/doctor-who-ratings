namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.ShareByHoursWatched;

public static class ShareByHoursWatchedChartOptions
{
    public static ApexChartOptions<ShareByHoursWatchedDataPoint> Defaults =>
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
