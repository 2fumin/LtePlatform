app.controller('workitem.cell', function ($scope, networkElementService, $routeParams) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryCellInfo($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
});