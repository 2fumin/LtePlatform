app.controller('kpi.topConnection3G.trend', function ($scope, $routeParams, appRegionService, appFormatService, connection3GService) {
    $scope.page.title = "TOP连接变化趋势-" + $routeParams.city;
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
    $scope.showTrend = function () {
        connection3GService.queryCellTrend($scope.beginDate.value, $scope.endDate.value, $routeParams.city,
            $scope.orderPolicy.selected, $scope.topCount.selected).then(function (result) {
                $scope.trendCells = result;
            });
    };
    connection3GService.queryOrderPolicy().then(function (result) {
        $scope.orderPolicy = {
            options: result,
            selected: result[0]
        }
        $scope.showTrend();
    });
});