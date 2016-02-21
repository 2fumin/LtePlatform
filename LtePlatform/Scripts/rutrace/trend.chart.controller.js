
app.controller("rutrace.trendchart", function ($scope, $location, appKpiService) {
    if ($scope.trendStat.mrStats.length === 0) $location.path($scope.rootPath + "trend");

    $scope.showCharts = function () {
        $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
        $("#precise").highcharts(appKpiService.getPreciseRateOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
    };
    $scope.timeMrConfig = appKpiService.getMrsDistrictOptions($scope.trendStat.mrStats,
        $scope.trendStat.districts);
    $scope.timePreciseConfig = appKpiService.getPreciseDistrictOptions($scope.trendStat.preciseStats,
        $scope.trendStat.districts);
});
