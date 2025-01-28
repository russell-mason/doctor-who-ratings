var ApexChartExtensions = /** @class */ (function () {
    function ApexChartExtensions() {
    }
    ApexChartExtensions.selectDataPoint = function (chartId, index) {
        var chart = ApexCharts.getChartByID(chartId);
        chart.toggleDataPointSelection(0, index);
    };
    ApexChartExtensions.disableDataPointSelection = function (chartId) {
        var chartElement = document.getElementById(chartId);
        chartElement.addEventListener("mousedown", function (event) {
            event.stopPropagation();
        }, true);
    };
    return ApexChartExtensions;
}());
//# sourceMappingURL=apexchart-extensions.js.map