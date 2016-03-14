app.controller("cell.trend", function ($scope, $routeParams, appKpiService, cellPreciseService, appFormatService) {
    $scope.page.title = "小区指标变化趋势分析" + "-" + $routeParams.name;
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.showTrend = function() {
        $scope.beginDateString = appFormatService.getDateString($scope.beginDate.value, "yyyy年MM月dd日");
        $scope.endDateString = appFormatService.getDateString($scope.endDate.value, "yyyy年MM月dd日");
        cellPreciseService.queryDataSpanKpi($scope.beginDate.value, $scope.endDate.value, $routeParams.cellId,
            $routeParams.sectorId).then(function (result) {
                $scope.mrsConfig = cellPreciseService.getMrsOptions(result,
                    $scope.beginDateString + "-" + $scope.endDateString + "MR数变化趋势");
                $scope.preciseConfig = cellPreciseService.getPreciseOptions(result,
                    $scope.beginDateString + "-" + $scope.endDateString + "精确覆盖率变化趋势");
        });
    };
    $scope.showTrend();
});