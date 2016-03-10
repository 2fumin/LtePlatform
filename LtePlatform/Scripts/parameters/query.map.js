app.controller("query.map", function($scope, appRegionService) {
    $scope.page.title = "小区地图查询";
    $scope.network = {
        options: ["LTE", "CDMA"],
        selected: "LTE"
    };
    
    $scope.updateDistricts = function() {
        appRegionService.queryDistricts($scope.city.selected).then(function(result) {
            $scope.district.options = result;
            $scope.district.selected = result[0];
        });
    };
    $scope.updateTowns = function() {
        appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(result) {
            $scope.town.options = result;
            $scope.town.selected = result[0];
        });
    };
    $scope.queryItems = function() {

    };

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
        appRegionService.queryDistricts($scope.city.selected).then(function (districts) {
            $scope.district = {
                options: districts,
                selected: districts[0]
            };
            appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function (towns) {
                $scope.town = {
                    options: towns,
                    selected: towns[0]
                };
            });
        });
    });
});