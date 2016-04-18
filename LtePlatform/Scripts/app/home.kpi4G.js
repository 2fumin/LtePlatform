app.controller("home.kpi4G", function ($scope, appKpiService, appFormatService, kpi4GDisplayService) {
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
        .then(function(result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            $scope.cityStat = appKpiService.getCityStat(result.districtPreciseViews, $scope.city.selected);
            $scope.rate = appKpiService.calculatePreciseRating($scope.cityStat.preciseRate);
            $("#preciseConfig").highcharts(kpi4GDisplayService.generatePreciseBarOptions(result.districtPreciseViews,
                $scope.cityStat));
        });
});