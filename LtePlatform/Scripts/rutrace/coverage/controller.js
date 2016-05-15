app.controller("rutrace.coverage", function($scope, $timeout, $routeParams) {
    $scope.currentCellName = $routeParams.name + "-" + $routeParams.sectorId;
    $scope.page.title = "TOP指标覆盖分析: " + $scope.currentCellName;
});