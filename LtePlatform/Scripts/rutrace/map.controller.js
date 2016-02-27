app.controller("rutrace.map", function ($scope, $timeout, $routeParams, $location, $uibModal, $log, baiduMapService, networkElementService) {
    var cell = $scope.topStat.current;
    $scope.range = {};
    if ($routeParams.name !== cell.eNodebName + "-" + cell.sectorName) {
        if ($scope.topStat.cells[$routeParams.name] === undefined) {
            $location.path('/Rutrace#/top');
        }
        $scope.topStat.current = $scope.topStat.cells[$routeParams.name];
    }
    $scope.page.title = "小区地理化分析"+ "-" + $routeParams.name;
    $scope.updateMenuItems("小区地理化分析", $scope.rootPath + "baidumap", $routeParams.name);

    $scope.showPrecise = function(precise) {
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

    networkElementService.queryCellSectors([$scope.topStat.current]).then(function (result) {
        baiduMapService.initializeMap("all-map", 12);
        var sectorTriangle = baiduMapService.generateSector(result[0], "blue");
        baiduMapService.addOneSectorToScope(sectorTriangle, $scope.showPrecise, result[0]);

        baiduMapService.setCellFocus(result[0].baiduLongtitute, result[0].baiduLattitute, 16);
        //$scope.range = baiduMapService.getCurrentMapRange();
    });

    //$scope.$watch("range", function(range) {
    //    networkElementService.queryRangeSectors(range, [
    //        {
    //            cellId: $scope.topStat.current.cellId,
    //            sectorId: $scope.topStat.current.sectorId
    //        }
    //    ]).then(function(sectors) {
    //        for (var i = 0; i < sectors.length; i++) {
    //            baiduMapService.addOneSectorToScope(baiduMapService.generateSector(sectors[i], "green"), $scope.showNeighbor, sectors[i]);
    //        }
    //    });
        
    //});
            
    $scope.toggleNeighbors = function() {
    };
});