app.controller("rutrace.interference", function ($scope, $timeout, $routeParams,
    networkElementService, topPreciseService, menuItemService, neighborMongoService) {
    $scope.currentCellName = $routeParams.name + "-" + $routeParams.sectorId;
    $scope.page.title = "TOP指标干扰分析: " + $scope.currentCellName;
    menuItemService.updateMenuItem($scope.menuItems, 1, $scope.page.title,
        $scope.rootPath + "interference/" + $routeParams.cellId + "/" + $routeParams.sectorId + "/" + $routeParams.name);
    $scope.oneAtATime = false;
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    var options = [
        {
            name: "模3干扰数",
            value: "mod3Interferences"
        }, {
            name: "模6干扰数",
            value: "mod6Interferences"
        }, {
            name: "6dB干扰数",
            value: "overInterferences6Db"
        }, {
            name: "10dB干扰数",
            value: "overInterferences10Db"
        }, {
            name: "总干扰水平",
            value: "interferenceLevel"
        }
    ];
    $scope.orderPolicy = {
        options: options,
        selected: options[4].value
    };
    $scope.updateMessages = [];

    $scope.showInterference = function() {
        $scope.interferenceCells = [];
        $scope.victimCells = [];

        topPreciseService.queryInterferenceNeighbor($scope.beginDate.value, $scope.endDate.value,
            $routeParams.cellId, $routeParams.sectorId).then(function(result) {
            angular.forEach(result, function(cell) {
                for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                    var neighbor = $scope.mongoNeighbors[i];
                    if (neighbor.neighborPci === cell.destPci) {
                        cell.isMongoNeighbor = true;
                        break;
                    }
                }
            });
            $scope.interferenceCells = result;
            $scope.topStat.interference[$scope.currentCellName] = result;
            topPreciseService.queryInterferenceVictim($scope.beginDate.value, $scope.endDate.value,
                $routeParams.cellId, $routeParams.sectorId).then(function(victims) {
                $scope.victimCells = victims;
                $scope.topStat.victims[$scope.currentCellName] = victims;
                });
            var pieOptions = topPreciseService.getInterferencePieOptions(result, $scope.currentCellName);
            $scope.topStat.pieOptions[$scope.currentCellName] = pieOptions;
            $("#interference-over6db").highcharts(pieOptions.over6DbOption);
            $("#interference-over10db").highcharts(pieOptions.over10DbOption);
            $("#interference-mod3").highcharts(pieOptions.mod3Option);
            $("#interference-mod6").highcharts(pieOptions.mod6Option);
        });
    };

    $scope.updateNeighborInfos=function() {
        if ($scope.topStat.updateInteferenceProgress[$scope.currentCellName] !== true) {
            $scope.topStat.updateInteferenceProgress[$scope.currentCellName] = true;
            topPreciseService.updateInterferenceNeighbor($routeParams.cellId, $routeParams.sectorId).then(function (result) {
                $scope.updateMessages.push({
                    cellName: $scope.currentCellName,
                    counts: result,
                    type: "干扰"
                });
                $scope.topStat.updateInteferenceProgress[$scope.currentCellName] = false;
            });
        }

        if ($scope.topStat.updateVictimProgress[$scope.currentCellName] !== true) {
            $scope.topStat.updateVictimProgress[$scope.currentCellName] = true;
            topPreciseService.updateInterferenceVictim($routeParams.cellId, $routeParams.sectorId).then(function (result) {
                $scope.updateMessages.push({
                    cellName: $scope.currentCellName,
                    counts: result,
                    type: "被干扰"
                });
                $scope.topStat.updateVictimProgress[$scope.currentCellName] = false;
            });
        }

    }

    $scope.closeAlert = function(index) {
        $scope.updateMessages.splice(index, 1);
    };

    if ($scope.topStat.interference[$scope.currentCellName] === undefined) {
        neighborMongoService.queryNeighbors($routeParams.cellId, $routeParams.sectorId).then(function(result) {
            $scope.mongoNeighbors = result;
            $scope.topStat.mongoNeighbors[$scope.currentCellName] = result;
            $scope.showInterference();
            $scope.updateNeighborInfos();
        });
    } else {
        $scope.interferenceCells = $scope.topStat.interference[$scope.currentCellName];
        $scope.victimCells = $scope.topStat.victims[$scope.currentCellName];
        $scope.mongoNeighbors = $scope.topStat.mongoNeighbors[$scope.currentCellName];
        var newOptions = $scope.topStat.pieOptions[$scope.currentCellName];
        $timeout(function() {
            $("#interference-over6db").highcharts(newOptions.over6DbOption);
            $("#interference-over10db").highcharts(newOptions.over10DbOption);
            $("#interference-mod3").highcharts(newOptions.mod3Option);
            $("#interference-mod6").highcharts(newOptions.mod6Option);
        }, 1000);
    }
    networkElementService.queryCellInfo($routeParams.cellId, $routeParams.sectorId).then(function(info) {
        $scope.topStat.current = {
            cellId: $routeParams.cellId,
            sectorId: $routeParams.sectorId,
            eNodebName: $routeParams.name,
            longtitute: info.longtitute,
            lattitute: info.lattitute
        };
    });
});