app.controller("parameters.list", function ($scope, appRegionService) {
    $scope.page.title = "基础数据总揽";

    $scope.showCityStats = function() {
        appRegionService.queryDistrictInfrastructures($scope.city.selected).then(function(result) {
            $scope.districtStats = result;
            $scope.showDistrictDetails(result[0].district);
        });
    };
    $scope.showDistrictDetails = function(district) {
        appRegionService.queryTownInfrastructures($scope.city.selected, district).then(function(result) {
            $scope.townStats = result;
        });
    };

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
        $scope.showCityStats();
    });
});