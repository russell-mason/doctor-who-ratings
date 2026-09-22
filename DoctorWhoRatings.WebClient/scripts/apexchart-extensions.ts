declare var ApexCharts: any;

class ApexChartExtensions {
    public static selectDataPoint(chartId: string, index: number): void {
        const chart = ApexCharts.getChartByID(chartId);

        chart.toggleDataPointSelection(0, index);
    }

    public static disableDataPointSelection(chartId: string): void {
        const chartElement = document.getElementById(chartId);

        if (chartElement) {
            chartElement.addEventListener("mousedown", function (event) {
                event.stopPropagation();
            }, true);
        }
    }
}

declare global {
    interface Window {
        ApexChartExtensions: typeof ApexChartExtensions;
    }
}

window.ApexChartExtensions = ApexChartExtensions;

export { };