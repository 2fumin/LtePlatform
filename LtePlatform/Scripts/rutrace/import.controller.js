app.controller("rutrace.import", function ($scope, $http, $routeParams,
    menuItemService, neighborService, neighborMongoService, topPreciseService, networkElementService, dumpPreciseService) {
    $scope.currentCellName = $routeParams.name + "-" + $routeParams.sectorId;
    $scope.page.title = "TOP指标邻区监控: " + $scope.currentCellName;
    menuItemService.updateMenuItem($scope.menuItems, 1, $scope.page.title,
        $scope.rootPath + "import/" + $routeParams.cellId + "/" + $routeParams.sectorId + "/" + $routeParams.name);
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: new Date(lastWeek.getFullYear(), lastWeek.getMonth(), lastWeek.getDate(), 8),
        opened: false
    };
    var today = new Date();
    $scope.endDate = {
        value: new Date(today.getFullYear(), today.getMonth(), today.getDate(), 8),
        opened: false
    };
    $scope.currentPage = 1;
    $scope.neighborCells = [];
    $scope.updateMessages = [];
    topPreciseService.queryMonitor($routeParams.cellId, $routeParams.sectorId).then(function(result) {
        $scope.cellMonitored = result;
    });

    $scope.showNeighbors = function() {
        $scope.neighborCells = [];
        neighborService.queryCellNeighbors($routeParams.cellId, $routeParams.sectorId).then(function (result) {
            $scope.neighborCells = result;
            angular.forEach(result, function(neighbor) {
                topPreciseService.queryMonitor(neighbor.cellId, neighbor.sectorId).then(function(monitored) {
                    neighbor.isMonitored = monitored;
                });
            });
        });
        
    };
    $scope.showReverseNeighbors=function() {
        neighborMongoService.queryReverseNeighbors($routeParams.cellId, $routeParams.sectorId).then(function (result) {
            $scope.reverseCells = result;
            angular.forEach(result, function(neighbor) {
                networkElementService.queryENodebInfo(neighbor.cellId).then(function(info) {
                    neighbor.eNodebName = info.name;
                });
                topPreciseService.queryMonitor(neighbor.cellId, neighbor.sectorId).then(function (monitored) {
                    neighbor.isMonitored = monitored;
                });
            });
        });
    }
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
        var count = 0;
        neighborMongoService.queryNeighbors($routeParams.cellId, $routeParams.sectorId).then(function(neighbors) {
            angular.forEach(neighbors, function (neighbor) {
                if (neighbor.neighborCellId > 0 && neighbor.neighborPci > 0) {
                    neighborService.updateNeighbors(neighbor.cellId, neighbor.sectorId, neighbor.neighborPci,
                        neighbor.neighborCellId, neighbor.neighborSectorId).then(function() {
                        count += 1;
                        if (count === neighbors.length) {
                            $scope.updateMessages.push({
                                cellName: $scope.currentCellName,
                                counts: count
                            });
                            $scope.showNeighbors();
                        }
                    });
                } else {
                    count += 1;
                    if (count === neighbors.length) {
                        $scope.updateMessages.push({
                            cellName: $scope.currentCellName,
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
        topPreciseService.addMonitor({
            cellId: $routeParams.cellId,
            sectorId: $routeParams.sectorId
        });
    };
    $scope.monitorNeighbors = function() {
        angular.forEach($scope.neighborCells, function(cell) {
            if (cell.isMonitored === false) {
                neighborService.monitorNeighbors(cell).then(function () {
                    cell.isMonitored = true;
                });
            }
        });
        angular.forEach($scope.reverseCells, function (cell) {
            if (cell.isMonitored === false) {
                neighborService.monitorNeighbors({
                    nearestCellId: cell.cellId,
                    nearestSectorId: cell.sectorId
                }).then(function () {
                    cell.isMonitored = true;
                });
            }
        });
    };

    $scope.dump = function() {
        networkElementService.queryCellInfo($routeParams.cellId, $routeParams.sectorId).then(function (info) {
            neighborMongoService.dumpMongoDialog({
                eNodebId: $routeParams.cellId,
                sectorId: $routeParams.sectorId,
                pci: info.pci,
                name: $routeParams.name
            }, $scope.beginDate.value, $scope.endDate.value);
        });
    };
    $scope.dumpMongo = function (cell) {
        neighborMongoService.dumpMongoDialog({
            eNodebId: cell.nearestCellId,
            sectorId: cell.nearestSectorId,
            pci: cell.pci,
            name: cell.nearestENodebName
        }, $scope.beginDate.value, $scope.endDate.value);
    };
    $scope.dumpReverseMongo = function (cell) {
        var begin = new Date($scope.beginDate.value);
        var end = new Date($scope.endDate.value);
        networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(info) {
            dumpPreciseService.dumpDateSpanSingleNeighborRecords(cell.cellId, cell.sectorId, info.pci, cell.neighborCellId,
                cell.neighborSectorId, cell.neighborPci, begin, end);
        });

    };

    $scope.showReverseNeighbors();
    $scope.showNeighbors();
});