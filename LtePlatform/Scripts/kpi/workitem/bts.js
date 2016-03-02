app.controller('workitem.bts', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryBtsInfo($routeParams.btsId).then(function (result) {
        $scope.btsDetails = result;
    });
    workitemService.queryByENodebId($routeParams.btsId).then(function (result) {
        $scope.viewItems = result;
    });
});