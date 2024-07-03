namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.TotalHoursWatchedByDoctor;

public static class TotalHoursWatchedByDoctorChartOptions
{
    public static ApexChartOptions<TotalHoursWatchedByDoctorDataPoint> Defaults =>
        new()
        {
            DataLabels = new DataLabels
            {
                DropShadow = new DropShadow
                {
                    Enabled = false
                },
                Formatter = "function(value) { return value.toFixed(2) + '%'; }"
            }
        };
}
