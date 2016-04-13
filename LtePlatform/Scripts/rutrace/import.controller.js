app.controller("rutrace.import", function ($scope, $http, $location, neighborService, neighborMongoService,
    topPreciseService, networkElementService) {
    $scope.neighborCells = [];
    $scope.updateMessages = [];

    $scope.showNeighbors = function() {
        var cell = $scope.topStat.current;
        $scope.neighborCells = [];
        neighborService.queryCellNeighbors(cell.cellId, cell.sectorId).then(function(result) {
            $scope.neighborCells = result;
        });
        neighborMongoService.queryReverseNeighbors(cell.cellId, cell.sectorId).then(function (result) {
            $scope.reverseCells = result;
            angular.forEach(result, function(neighbor) {
                networkElementService.queryENodebInfo(neighbor.cellId).then(function(info) {
                    neighbor.eNodebName = info.name;
                });
            });
        });
    };
    $scope.updatePci = function () {
        var cell = $scope.topStat.current;
        neighborService.updateCellPci(cell).then(function(result) {
            $scope.updateMessages.push({
                cellName: cell.eNodebName + '-' + cell.sectorId,
                counts: result
            });
            $scope.showNeighbors();
        });
    };
    $scope.synchronizeNeighbors = function () {
        var cell = $scope.topStat.current;
        var count = 0;
        neighborMongoService.queryNeighbors(cell.cellId, cell.sectorId).then(function(neighbors) {
            angular.forEach(neighbors, function (neighbor) {
                if (neighbor.neighborCellId > 0 && neighbor.neighborPci > 0) {
                    neighborService.updateNeighbors(neighbor.cellId, neighbor.sectorId, neighbor.neighborPci,
                        neighbor.neighborCellId, neighbor.neighborSectorId).then(function() {
                        count += 1;
                        if (count === neighbors.length) {
                            $scope.updateMessages.push({
                                cellName: cell.eNodebName + '-' + cell.sectorId,
                                counts: count
                            });
                            $scope.showNeighbors();
                        }
                    });
                } else {
                    count += 1;
                    if (count === neighbors.length) {
                        $scope.updateMessages.push({
                            cellName: cell.eNodebName + '-' + cell.sectorId,
                            counts: count
                        });
                        $scope.showNeighbors();
                    }
                }
            });
        });
    };
    $scope.closeAlert = function (index) {
        $scope.updateMessages.splice(index, 1);
    }
    $scope.addMonitor = function() {
        var cell = $scope.topStat.current;
        topPreciseService.addMonitor(cell);
    };
    $scope.addNeighborMonitor = function (cell) {
        neighborService.monitorNeighbors(cell).then(function() {
            cell.isMonitored = true;
        });
    };
    $scope.monitorNeighbors = function() {
        angular.forEach($scope.neighborCells.length, function(cell) {
            if (cell.isMonitored === false) {
                $scope.addNeighborMonitor(cell);
            }
        });
    };

    if ($scope.topStat.current.eNodebName === undefined || $scope.topStat.current.eNodebName === "")
        $location.path($scope.rootPath + "top");
    else {
        $scope.showNeighbors();
    }
        
});