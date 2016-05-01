app.controller('workitem.eNodeb', function ($scope, networkElementService, $routeParams, workitemService,
    workItemDialog) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByENodebId($routeParams.eNodebId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    $scope.showDetails = function(view) {
        workItemDialog.showDetails(view);
    };
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    $scope.queryWorkItems();
});