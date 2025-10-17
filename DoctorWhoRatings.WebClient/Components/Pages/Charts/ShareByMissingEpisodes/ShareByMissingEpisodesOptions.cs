namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.ShareByMissingEpisodes;

public static class ShareByMissingEpisodesChartOptions
{
    public static ApexChartOptions<ShareByMissingEpisodesDataPoint> Defaults =>
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
