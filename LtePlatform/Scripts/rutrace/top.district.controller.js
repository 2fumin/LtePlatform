app.controller("rutrace.top.district", function($scope, $routeParams, $http, appUrlService, topPreciseService) {
    $scope.page.title = "TOP指标分析-" + $routeParams.district;
    $scope.topCells = [];
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
    $scope.orderPolicy = {
        options: [],
        selected: ""
    };
    $scope.topCount = {
        options: [5, 10, 15, 20],
        selected: 5
    };

    $scope.query = function () {
        $scope.topCells = [];
        topPreciseService.queryTopKpisInDistrict($scope.beginDate.value, $scope.endDate.value, $scope.topCount.selected,
            $scope.orderPolicy.selected, $scope.overallStat.city, $routeParams.district).then(function (result) {
                $scope.topCells = result;
            });
    };
    $scope.monitorAll = function () {
        for (var i = 0; i < $scope.topCells.length; i++) {
            var cell = $scope.topCells[i];
            if (cell.isMonitored === false) {
                topPreciseService.addMonitor(cell);
            }
        }
    };
    $scope.updateInfo = function (cell) {
        $scope.topStat.current = cell;
        $scope.updateTopCells(cell);
    };

    topPreciseService.getOrderSelection().then(function (result) {
        $scope.orderPolicy.options = result;
        $scope.orderPolicy.selected = result[0];
        $scope.query();
    });
});