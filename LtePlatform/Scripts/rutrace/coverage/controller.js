app.controller("rutrace.coverage", function($scope, $timeout, $routeParams, topPreciseService) {
    $scope.currentCellName = $routeParams.name + "-" + $routeParams.sectorId;
    $scope.page.title = "TOP指标覆盖分析: " + $scope.currentCellName;
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
    $scope.showCoverage=function() {
        topPreciseService.queryCoverage($scope.beginDate.value, $scope.endDate.value,
            $routeParams.cellId, $routeParams.sectorId).then(function (result) {
            $scope.coverageList = result;
        });
    }
});