app.controller("kpi.workitem.chart", function ($scope, $timeout, showPieChart, workitemService) {
    $scope.page.title = "统计图表";
    if ($scope.viewData.chartData === undefined) {
        workitemService.queryChartData().then(function(result) {
            $scope.viewData.chartData = result;
            showPieChart.type(result, "#type-chart");
            showPieChart.state(result, "#state-chart");
        });
    } else {
        $timeout(function() {
            showPieChart.type($scope.viewData.chartData, "#type-chart");
            showPieChart.state($scope.viewData.chartData, "#state-chart");
        }, 1000);
    }

});