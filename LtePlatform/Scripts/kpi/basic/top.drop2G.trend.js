app.controller('kpi.topDrop2G.trend', function ($scope, $routeParams, appRegionService, appFormatService, drop2GService) {
    for (var i = 0; i < $scope.topData.drop2G.length; i++) {
        var cell = $scope.topData.drop2G[i];
        if (cell.cellId === parseInt($routeParams.cellId) && cell.sectorId === parseInt($routeParams.sectorId)) {
            $scope.page.title = "小区" + cell.cdmaName + "-" + cell.sectorId + "指标变化趋势";
            break;
        }
    }
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
    $scope.topCount = {
        options: [10, 20, 30, 50],
        selected: 10
    };
    drop2GService.queryOrderPolicy().then(function(result) {
        $scope.orderPolicy = {
            options: result,
            selected: result[0]
        }
    });
    $scope.showTrend = function() {
        drop2GService.queryCellTrend($scope.beginDate.value, $scope.endDate.value, $routeParams.city,
            $scope.orderPolicy.selected, $scope.topCount.selected).then(function (result) {
            $scope.trendCells = result;
        });
    };
});