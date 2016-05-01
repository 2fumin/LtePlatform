app.controller('workitem.bts', function ($scope, networkElementService, $routeParams, workitemService,
    workItemDialog) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByENodebId($routeParams.btsId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    $scope.showDetails = function (view) {
        workItemDialog.showDetails(view);
    };
    networkElementService.queryBtsInfo($routeParams.btsId).then(function (result) {
        $scope.btsDetails = result;
    });
    $scope.queryWorkItems();
});