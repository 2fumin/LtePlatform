app.controller('workitem.cdmaCell', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryCdmaCellInfo($routeParams.btsId, $routeParams.sectorId).then(function (result) {
        $scope.cdmaCellDetails = result;
    });
    workitemService.queryByCellId($routeParams.btsId, $routeParams.sectorId).then(function (result) {
        $scope.viewItems = result;
    });
});