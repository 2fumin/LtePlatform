
app.controller("rutrace.root", function ($scope, appRegionService, menuItemService) {
    $scope.page = { title: "指标总体情况" };
    $scope.overallStat = {
        currentDistrict: "",
        districtStats: [],
        townStats: [],
        cityStat: {},
        dateString: "",
        city: ""
    };
    $scope.trendStat = {
        stats: [],
        districts: [],
        districtStats: [],
        townStats: [],
        beginDateString: "",
        endDateString: ""
    };
    $scope.topStat = {
        current: {},
        cells: {},
        interference: {},
        victims: {},
        coverages: {},
        updateInteferenceProgress: {},
        updateVictimProgress: {},
        mongoNeighbors: {},
        pieOptions: {},
        columnOptions: {}
    };
    $scope.updateTopCells = function(cell) {
        var cellName = cell.eNodebName + "-" + cell.sectorId;
        if ($scope.topStat.cells[cellName] === undefined) {
            $scope.topStat.cells[cellName] = cell;
        }
    };

    appRegionService.initializeCities().then(function(result) {
        $scope.overallStat.city = result.selected;
        appRegionService.queryDistricts(result.selected).then(function (districts) {
            for (var i = 0; i < districts.length; i++) {
                menuItemService.updateMenuItem($scope.menuItems, 0,
                    "TOP指标分析-" + districts[i],
                    $scope.rootPath + "topDistrict/" + districts[i]);
            }
        });
    });
});
