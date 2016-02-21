app.controller("rutrace.top", function ($scope, $http, appUrlService, topPreciseService) {
    $scope.page.title = "TOP指标分析";
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
        options: [5, 10, 15, 20, 30],
        selected: 15
    };

    $scope.query = function () {
        $scope.topCells = [];
        topPreciseService.queryTopKpis($scope.beginDate.value, $scope.endDate.value, $scope.topCount.selected,
            $scope.orderPolicy.selected).then(function (result) {
                $scope.topCells = result;
            });
    };
    $scope.monitorAll = function () {
        for (var i = 0; i < $scope.topCells.length; i++) {
            var cell = $scope.topCells[i];
            if (cell.isMonitored === false) {
                $scope.addMonitor(cell);
            }
        }
    };

    topPreciseService.getOrderSelection().then(function (result) {
        $scope.orderPolicy.options = result;
        $scope.orderPolicy.selected = result[0];
        $scope.query();
    });
});