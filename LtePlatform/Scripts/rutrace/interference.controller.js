app.controller("rutrace.interference", function ($scope, $http, $location, $routeParams,
    networkElementService, topPreciseService, menuItemService, neighborMongoService) {
    $scope.currentCellName = $routeParams.name + "-" + $routeParams.sectorId;
    $scope.page.title = "TOP指标干扰分析: " + $scope.currentCellName;
    menuItemService.updateMenuItem($scope.menuItems, 1, $scope.page.title, 
        $scope.rootPath + $routeParams.cellId + "/" + $routeParams.sectorId + "/" +$routeParams.name)
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
            $scope.interferenceCells = result;
            $scope.topStat.interference[$scope.currentCellName] = result;
        });

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

        topPreciseService.queryInterferenceVictim($scope.beginDate.value, $scope.endDate.value,
            $routeParams.cellId, $routeParams.sectorId).then(function (result) {
            $scope.victimCells = result;
            $scope.topStat.victims[$scope.currentCellName] = result;
        });

    };

    $scope.closeAlert = function(index) {
        $scope.updateMessages.splice(index, 1);
    };

    neighborMongoService.queryNeighbors($routeParams.cellId, $routeParams.sectorId).then(function (result) {
        $scope.mongoNeighbors = result;
        if ($scope.topStat.interference[$scope.currentCellName] === undefined) {
            $scope.showInterference();
        } else {
            $scope.interferenceCells = $scope.topStat.interference[$scope.currentCellName];
            $scope.victimCells = $scope.topStat.victims[$scope.currentCellName];
        }
    });
});