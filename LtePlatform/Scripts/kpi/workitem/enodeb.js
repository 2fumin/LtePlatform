app.controller('workitem.eNodeb', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    workitemService.queryByENodebId($routeParams.eNodebId).then(function (result) {
        $scope.viewItems = result;
    });
});