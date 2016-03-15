app.controller("parameters.list", function ($scope, appRegionService, parametersChartService) {
    $scope.page.title = "基础数据总揽";

    $scope.showCityStats = function() {
        appRegionService.queryDistrictInfrastructures($scope.city.selected).then(function(result) {
            $scope.districtStats = result;
            $scope.showDistrictDetails(result[0].district);
            $scope.cityLteENodebConfig = parametersChartService.getDistrictLteENodebPieOptions(result.slice(0, result.length - 1), $scope.city.selected);
            $scope.cityLteCellConfig = parametersChartService.getDistrictLteCellPieOptions(result.slice(0, result.length - 1), $scope.city.selected);
            $scope.cityCdmaENodebConfig = parametersChartService.getDistrictCdmaBtsPieOptions(result.slice(0, result.length - 1), $scope.city.selected);
            $scope.cityCdmaCellConfig = parametersChartService.getDistrictCdmaCellPieOptions(result.slice(0, result.length - 1), $scope.city.selected);
        });
    };
    $scope.showDistrictDetails = function(district) {
        appRegionService.queryTownInfrastructures($scope.city.selected, district).then(function(result) {
            $scope.townStats = result;
            $scope.districtLteENodebConfig = parametersChartService.getTownLteENodebPieOptions(result, district);
            $scope.districtLteCellConfig = parametersChartService.getTownLteCellPieOptions(result, district);
            $scope.districtCdmaENodebConfig = parametersChartService.getTownCdmaBtsPieOptions(result, district);
            $scope.districtCdmaCellConfig = parametersChartService.getTownCdmaCellPieOptions(result, district);
        });
    };

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
        $scope.showCityStats();
    });
});