namespace DoctorWhoRatings.WebClient.Components.Extensions;

public static class ApexChartOptionsExtensions
{
    public static List<AnnotationsXAxis> CreateAnnotationsXAxis(IEnumerable<(int index, string text)> items) =>
        items.Select(item => CreateAnnotationsXAxis(item.index, item.text)).ToList();

    private static AnnotationsXAxis CreateAnnotationsXAxis(int index, string text) =>
        new()
        {
            Label = new Label
            {
                Text = text,
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
