
app.controller("rutrace.trendchart", function ($scope, $location, $timeout, appKpiService) {

    $scope.showCharts = function () {
        $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
        $("#precise").highcharts(appKpiService.getPreciseRateOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
    };
    $scope.timeMrConfig = appKpiService.getMrsDistrictOptions($scope.trendStat.stats,
        $scope.trendStat.districts);
    $scope.timePreciseConfig = appKpiService.getPreciseDistrictOptions($scope.trendStat.stats,
        $scope.trendStat.districts);
    $timeout(function() {
        $scope.showCharts();
    }, 1000);
});
