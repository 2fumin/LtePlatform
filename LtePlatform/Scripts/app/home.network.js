app.controller("home.network", function ($scope, appRegionService, parametersChartService) {
    appRegionService.queryDistrictInfrastructures($scope.city.selected || '佛山').then(function (result) {
        $("#cityLteENodebConfig").highcharts(parametersChartService.getDistrictLteENodebPieOptions(result.slice(0, result.length - 1),
            $scope.city.selected || '佛山'));
    });
});