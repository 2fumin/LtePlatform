app.controller("home.kpi4G", function ($scope, appKpiService, appFormatService, kpi4GDisplayService) {
    appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
        .then(function(result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            $scope.cityStat = appKpiService.getCityStat(result.districtPreciseViews, $scope.city.selected);
            $("#preciseConfig").highcharts(kpi4GDisplayService.generatePreciseBarOptions(result.districtPreciseViews,
                $scope.cityStat));
        });
});