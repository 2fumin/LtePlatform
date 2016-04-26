app.controller('workitem.cdmaCell', function ($scope, networkElementService, $routeParams, workitemService,
    workItemDialog) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByCellId($routeParams.btsId, $routeParams.sectorId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    networkElementService.queryCdmaCellInfo($routeParams.btsId, $routeParams.sectorId).then(function (result) {
        $scope.cdmaCellDetails = result;
    });
    $scope.queryWorkItems();
});