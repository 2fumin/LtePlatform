app.controller("rutrace.map", function ($scope, $timeout, $routeParams, $location, $uibModal, $log,
    geometryService, baiduMapService, networkElementService, menuItemService, cellPreciseService) {
    $scope.page.title = "小区地理化分析" + ": " + $routeParams.name + "-" + $routeParams.sectorId;
    menuItemService.updateMenuItem($scope.menuItems, 1,
        $scope.page.title,
        $scope.rootPath + "baidumap/" + $routeParams.cellId + "/" + $routeParams.sectorId + "/" + $routeParams.name);

    $scope.showPrecise = function (precise) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Rutrace/Map/PreciseSectorMapInfoBox.html',
            controller: 'map.precise.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return precise.eNodebName + "-" + precise.sectorId + "精确覆盖率指标";
                },
                precise: function () {
                    return precise;
                }
            }
        });
        modalInstance.result.then(function (sector) {
            console.log(sector);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });

    };

    $scope.showNeighbor = function (neighbor) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Rutrace/Map/NeighborMapInfoBox.html',
            controller: 'map.neighbor.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return neighbor.cellName + "小区信息";
                },
                neighbor: function () {
                    return neighbor;
                }
            }
        });
        modalInstance.result.then(function (nei) {
            console.log(nei);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    cellPreciseService.queryOneWeekKpi($routeParams.cellId, $routeParams.sectorId).then(function(cellView) {
        networkElementService.queryCellSectors([cellView]).then(function (result) {
            geometryService.transformToBaidu(result[0].longtitute, result[0].lattitute).then(function (coors) {
                baiduMapService.initializeMap("all-map", 12);
                var xOffset = coors.x - result[0].longtitute;
                var yOffset = coors.y - result[0].lattitute;
                result[0].longtitute = coors.x;
                result[0].lattitute = coors.y;

                var sectorTriangle = baiduMapService.generateSector(result[0], "blue", 1.25);
                baiduMapService.addOneSectorToScope(sectorTriangle, $scope.showPrecise, result[0]);

                baiduMapService.setCellFocus(result[0].longtitute, result[0].lattitute, 15);
                var range = baiduMapService.getCurrentMapRange(-xOffset, -yOffset);

                networkElementService.queryRangeSectors(range, []).then(function (sectors) {
                    angular.forEach(sectors, function(sector) {
                        sector.longtitute += xOffset;
                        sector.lattitute += yOffset;
                        baiduMapService.addOneSectorToScope(baiduMapService.generateSector(sector, "green"),
                            $scope.showNeighbor, sector);
                    });
                });
            });

        });
    });
    

            
    $scope.toggleNeighbors = function() {
    };
});