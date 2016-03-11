app.controller("query.map", function ($scope, $uibModal, $log, appRegionService, baiduMapService, parametersMapService) {
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
    $scope.showENodebInfo = function(eNodeb) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Parameters/Map/ENodebMapInfoBox.html',
            controller: 'map.eNodeb.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return eNodeb.name + "-" + "基站基本信息";
                },
                eNodeb: function () {
                    return eNodeb;
                }
            }
        });
        modalInstance.result.then(function (info) {
            console.log(info);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.showCellInfo = function(cell) {

    };
    $scope.queryItems = function() {
        baiduMapService.clearOverlays();
        if ($scope.network.selected === "LTE") {
            if ($scope.queryText.trim() === "") {
                parametersMapService.showElementsInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected,
                    $scope.showENodebInfo, $scope.showCellInfo);
            } else {
                
            }
        } else {
            if ($scope.queryText.trim() === "") {

            } else {

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