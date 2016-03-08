app.controller("rutrace.workitems", function ($scope, $routeParams, workitemService) {
    $scope.page.title = "TOP小区工单历史";

    workitemService.queryByCellId($routeParams.cellId, $routeParams.sectorId).then(function(result) {
        $scope.viewItems = result;
    });
});