
app.controller("rutrace.trend", function ($scope, appRegionService, appKpiService, appFormatService) {
    $scope.page.title = "指标变化趋势";
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

    };
    appRegionService.initializeCities()
        .then(function (result) {
            $scope.city = result;
            $scope.showTrend();
        });
})