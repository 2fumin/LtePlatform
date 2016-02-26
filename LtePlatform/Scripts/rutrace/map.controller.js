app.controller("rutrace.map", function ($scope, $timeout, $routeParams, $location, baiduMapService, networkElementService) {
    var cell = $scope.topStat.current;
    $scope.preciseSector = {};
    $scope.range = {};
    $scope.neighbor = {};
    if ($routeParams.name !== cell.eNodebName + "-" + cell.sectorName) {
        if ($scope.topStat.cells[$routeParams.name] === undefined) {
            $location.path('/Rutrace#/top');
        }
        $scope.topStat.current = $scope.topStat.cells[$routeParams.name];
    }
    $scope.page.title = "小区地理化分析"+ "-" + $routeParams.name;
    $scope.updateMenuItems("小区地理化分析", $scope.rootPath + "baidumap", $routeParams.name);
    baiduMapService.initializeMap("all-map", 12);
    networkElementService.queryCellSectors([$scope.topStat.current]).then(function(result) {
        $scope.preciseSector = result[0];
        $timeout(function() {
            var html = $("#sector-info-box").html();
            baiduMapService.addOneSector(baiduMapService.generateSector($scope.preciseSector), html, "400px");
            baiduMapService.setCellFocus($scope.preciseSector.baiduLongtitute, $scope.preciseSector.baiduLattitute, 16);
            $scope.range = baiduMapService.getCurrentMapRange();
        }, 1000);
    });

    $scope.showNeighbor = function (neighbor) {
        console.log(neighbor);
        
        $scope.neighbor = neighbor;
        $timeout(function() {
            var html = $("#neighbor-info-box").html();
            console.log(html);
        }, 1000);
    };
    $scope.$watch("range", function(range) {
        networkElementService.queryRangeSectors(range, [
            {
                cellId: $scope.topStat.current.cellId,
                sectorId: $scope.topStat.current.sectorId
            }
        ]).then(function(sectors) {
            for (var i = 0; i < sectors.length; i++) {
                baiduMapService.addOneSectorToScope(baiduMapService.generateSector(sectors[i], "green"), $scope.showNeighbor, sectors[i]);
            }
        });
        
    });
            
    $scope.toggleNeighbors = function() {
    };
});