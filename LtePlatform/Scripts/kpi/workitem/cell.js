app.controller('workitem.cell', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryCellInfo($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
    workitemService.queryByCellId($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
        $scope.viewItems = result;
    });
});