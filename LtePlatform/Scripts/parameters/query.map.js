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

    $scope.showCellInfo = function(cell) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Rutrace/Map/NeighborMapInfoBox.html',
            controller: 'map.neighbor.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return cell.cellName + "小区信息";
                },
                neighbor: function () {
                    return cell;
                }
            }
        });
        modalInstance.result.then(function (nei) {
            console.log(nei);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.queryItems = function() {
        baiduMapService.clearOverlays();
        if ($scope.network.selected === "LTE") {
            if ($scope.queryText.trim() === "") {
                parametersMapService.showElementsInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected,
                    parametersDialogService.showENodebInfo, $scope.showCellInfo);
            } else {
                parametersMapService.showElementsWithGeneralName($scope.queryText, parametersDialogService.showENodebInfo, $scope.showCellInfo);
            }
        } else {
            if ($scope.queryText.trim() === "") {
                parametersMapService.showCdmaInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected,
                    parametersDialogService.showBtsInfo, $scope.showCellInfo);
            } else {
                parametersMapService.showCdmaWithGeneralName($scope.queryText, parametersDialogService.showBtsInfo, $scope.showCellInfo);
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