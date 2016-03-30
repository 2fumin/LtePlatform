app.controller("rutrace.import", function ($scope, $http, $location, neighborService, topPreciseService) {
    $scope.neighborCells = [];
    $scope.updateMessages = [];

    $scope.showNeighbors = function() {
        var cell = $scope.topStat.current;
        $scope.neighborCells = [];
        neighborService.queryCellNeighbors(cell.cellId, cell.sectorId).then(function(result) {
            $scope.neighborCells = result;
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
        for (var i = 0; i < $scope.neighborCells.length; i++) {
            var cell = $scope.neighborCells[i];
            if (cell.isMonitored === false) {
                $scope.addNeighborMonitor(cell);
            }
        }
    };

    if ($scope.topStat.current.eNodebName === undefined || $scope.topStat.current.eNodebName === "")
        $location.path($scope.rootPath + "top");
    else {
        $scope.showNeighbors();
    }
        
});