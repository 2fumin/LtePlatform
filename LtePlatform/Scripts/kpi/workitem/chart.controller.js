app.controller("kpi.workitem.chart", function ($scope, showPieChart, workitemService) {
    if ($scope.viewData.chartData === undefined) {
        workitemService.queryChartData().then(function (result) {
            $scope.viewData.chartData = result;
        });
    }
    showPieChart.type($scope.viewData.chartData, "#type-chart");
    showPieChart.state($scope.viewData.chartData, "#state-chart");
});