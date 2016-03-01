app.controller('workitem.cdmaCell', function ($scope, networkElementService, $routeParams) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryCdmaCellInfo($routeParams.btsId, $routeParams.sectorId).then(function (result) {
        $scope.cdmaCellDetails = result;
    });
});