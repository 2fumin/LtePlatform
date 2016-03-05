app.controller('kpi.topDrop2G', function ($scope, appRegionService, appFormatService, drop2GService) {
    $scope.page.title = "TOP掉话指标";
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
    $scope.showKpi = function() {
        drop2GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function (result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            $scope.topData.drop2G = result.statViews;
        });
    };
    appRegionService.initializeCities().then(function (result) {
        $scope.city = result;
        $scope.showKpi();
    });
});