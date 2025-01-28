declare var ApexCharts: any;

class ApexChartExtensions {
    public static selectDataPoint(chartId: string, index: number): void {
        let chart = ApexCharts.getChartByID(chartId);

        chart.toggleDataPointSelection(0, index);
    }

    public static disableDataPointSelection(chartId: string): void {
        let chartElement = document.getElementById(chartId);

        chartElement.addEventListener("mousedown", function (event) {
            event.stopPropagation();
        }, true);
    }
}