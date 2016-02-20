
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
    $scope.districts = [];

    $scope.showTrend = function () {
        $scope.trendStat.mrStats = [];
        $scope.trendStat.preciseStats = [];
        appKpiService.getDateSpanPreciseRegionKpi($scope.city.selected, $scope.beginDate.value, $scope.endDate.value)
            .then(function (result) {
                appKpiService.generateDistrictStats($scope.trendStat, $scope.districts, result);
                if (result.length > 0) {
                    appKpiService.generateTrendStatsForPie($scope.trendStat, result);
                }
            });
    };
    appRegionService.initializeCities()
        .then(function (result) {
            $scope.city = result;
            var city = $scope.city.selected;
            appRegionService.queryDistricts(city)
                .then(function (districts) {
                    districts.push(city);
                    $scope.districts = districts;
                    $scope.showTrend();
            });
        });
})