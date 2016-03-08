app.controller('kpi.topConnection3G', function ($scope, appRegionService, appFormatService, connection3GService) {
    $scope.page.title = "TOP连接成功率指标";
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    $scope.city = {
        selected: "",
        options: []
    };
    $scope.showKpi = function () {
        connection3GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function (result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            $scope.topData.connection3G = result.statViews;
        });
    };
    appRegionService.initializeCities().then(function (result) {
        $scope.city = result;
        $scope.showKpi();
    });
});