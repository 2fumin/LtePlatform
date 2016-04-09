app.controller("query.map", function ($scope, $uibModal, $log, appRegionService, baiduMapService, parametersMapService,
    parametersDialogService) {
    $scope.page.title = "小区地图查询";
    $scope.network = {
        options: ["LTE", "CDMA"],
        selected: "LTE"
    };
    $scope.queryText = "";
    
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
        baiduMapService.clearOverlays();
        if ($scope.network.selected === "LTE") {
            if ($scope.queryText.trim() === "") {
                parametersMapService.showElementsInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected,
                    parametersDialogService.showENodebInfo, parametersDialogService.showCellInfo);
            } else {
                parametersMapService.showElementsWithGeneralName($scope.queryText, parametersDialogService.showENodebInfo, parametersDialogService.showCellInfo);
            }
        } else {
            if ($scope.queryText.trim() === "") {
                parametersMapService.showCdmaInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected,
                    parametersDialogService.showBtsInfo, parametersDialogService.showCdmaCellInfo);
            } else {
                parametersMapService.showCdmaWithGeneralName($scope.queryText, parametersDialogService.showBtsInfo, parametersDialogService.showCdmaCellInfo);
            }
        }
    };

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
        baiduMapService.initializeMap("map", 12);
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