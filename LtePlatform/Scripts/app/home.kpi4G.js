app.controller("home.kpi4G", function ($scope, appKpiService, appFormatService) {
    appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
        .then(function(result) {
            $scope.statDate.value = appFormatService.getDate(result.statDate);
            console.log(result.districtPreciseViews);
        });
});