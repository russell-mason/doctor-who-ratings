namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.EpisodeInContext;

public interface IEpisodeContextDrillDown
{
    public Task DeselectAsync<T>(ApexChart<T> chart) where T : class;

    public Task DrillDownAsync<T>(SelectedData<T> selectedData, string? slug) where T : class;
}