class ApexChartExtensions {
    static selectDataPoint(chartId, index) {
        const chart = ApexCharts.getChartByID(chartId);
        chart.toggleDataPointSelection(0, index);
    }
    static disableDataPointSelection(chartId) {
        const chartElement = document.getElementById(chartId);
        if (chartElement) {
            chartElement.addEventListener("mousedown", function (event) {
                event.stopPropagation();
            }, true);
        }
    }
}
window.ApexChartExtensions = ApexChartExtensions;
export {};
//# sourceMappingURL=apexchart-extensions.js.map