app.controller("parameters.list", function ($scope, appRegionService, parametersChartService) {
    $scope.page.title = "基础数据总揽";

    $scope.showCityStats = function() {
        appRegionService.queryDistrictInfrastructures($scope.city.selected).then(function(result) {
            $scope.districtStats = result;
            $scope.showDistrictDetails(result[0].district);
            $("#cityLteENodebConfig").highcharts(parametersChartService.getDistrictLteENodebPieOptions(result.slice(0, result.length - 1),
                $scope.city.selected));
            $("#cityLteCellConfig").highcharts(parametersChartService.getDistrictLteCellPieOptions(result.slice(0, result.length - 1), 
                $scope.city.selected));
            $("#cityCdmaENodebConfig").highcharts(parametersChartService.getDistrictCdmaBtsPieOptions(result.slice(0, result.length - 1), 
                $scope.city.selected));
            $("#cityCdmaCellConfig").highcharts(parametersChartService.getDistrictCdmaCellPieOptions(result.slice(0, result.length - 1), 
                $scope.city.selected));
        });
    };
    $scope.showDistrictDetails = function (district) {
        $scope.currentDistrict = district;
        appRegionService.queryTownInfrastructures($scope.city.selected, district).then(function(result) {
            $scope.townStats = result;
            $("#districtLteENodebConfig").highcharts(parametersChartService.getTownLteENodebPieOptions(result, district));
            $("#districtLteCellConfig").highcharts(parametersChartService.getTownLteCellPieOptions(result, district));
            $("#districtCdmaENodebConfig").highcharts(parametersChartService.getTownCdmaBtsPieOptions(result, district));
            $("#districtCdmaCellConfig").highcharts(parametersChartService.getTownCdmaCellPieOptions(result, district));
        });
    };

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
        $scope.showCityStats();
    });
});