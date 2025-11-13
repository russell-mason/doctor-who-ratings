namespace DoctorWhoRatings.WebClient.Components.Pages.Charts.EpisodeInContext;

public class EpisodeContextDrillDown(IJSRuntime jsRuntime) : IEpisodeContextDrillDown
{
    public (int seriesIndex, int dataPointIndex) Selection { get; set; } = (-1, -1);

    public async Task DeselectAsync<T>(ApexChart<T> chart) where T : class
    {
        await chart.ToggleDataPointSelectionAsync(Selection.seriesIndex, Selection.dataPointIndex);
    }

    public async Task DrillDownAsync<T>(SelectedData<T> selectedData, string? slug) where T : class
    {
        if (!selectedData.IsSelected)
        {
            Selection = (-1, -1);

            return;
        }

        // Record the current selection so we can deselect it when the drop-down changes
        Selection = (selectedData.SeriesIndex, selectedData.DataPointIndex);

        var isShiftKeyDown = await jsRuntime.InvokeAsync<bool>("KeyboardEvents.isShiftKeyDown");

        if (isShiftKeyDown)
        {
            // Because this will open a new tab, which will start new tracking, we want to prevent any more
            // key events on this window, otherwise the window will be left in the incorrect tracking state.
            // This will be reapplied when the tab gets focus again
            await jsRuntime.InvokeVoidAsync("KeyboardEvents.untrack");

            await jsRuntime.InvokeVoidAsync("Browser.openTab", $"./charts/episodes/{slug}", slug);
        }
    }
}