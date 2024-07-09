namespace DoctorWhoRatings.WebClient.Components.Extensions;

public static class ApexChartOptionsExtensions
{
    public static List<AnnotationsXAxis> CreateAnnotationsXAxis(IEnumerable<(int index, string actor)> items) =>
        items.Select(item => CreateAnnotationsXAxis(item.index, item.actor)).ToList();

    private static AnnotationsXAxis CreateAnnotationsXAxis(int index, string actor) =>
        new()
        {
            Label = new Label
            {
                Text = actor,
                Style = new Style
                {
                    Color = "#707070"
                }
            },
            X = index,
            StrokeDashArray = 2,
            BorderColor = "#707070"
        };
}
