app.controller('kpi.topDrop2G.trend', function ($scope, $routeParams, appRegionService, appFormatService, kpi2GService) {
    for (var i = 0; i < $scope.topData.drop2G.length; i++) {
        var cell = $scope.topData.drop2G[i];
        if (cell.cellId === $routeParams.cellId && cell.sectorId === $routeParams.sectorId) {
            $scope.page.title = "小区" + cell.eNodebName + "-" + cell.sectorId + "指标变化趋势";
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
});