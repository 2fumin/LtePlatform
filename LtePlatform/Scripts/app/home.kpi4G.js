app.controller("home.kpi4G", function ($scope, appKpiService, appFormatService, kpi4GDisplayService) {
    appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
        .then(function(result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            $("#preciseConfig").highcharts(kpi4GDisplayService.generatePreciseBarOptions(result.districtPreciseViews,
                appKpiService.getCityStat(result.districtPreciseViews, $scope.city.selected)));
        });
});