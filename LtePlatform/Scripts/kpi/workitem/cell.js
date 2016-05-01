app.controller('workitem.cell', function ($scope, networkElementService, $routeParams, workitemService,
    workItemDialog) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByCellId($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    $scope.showDetails = function (view) {
        workItemDialog.showDetails(view);
    };
    networkElementService.queryCellInfo($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
    $scope.queryWorkItems();
});