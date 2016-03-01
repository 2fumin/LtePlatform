app.controller('workitem.bts', function ($scope, networkElementService, $routeParams) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryBtsInfo($routeParams.btsId).then(function (result) {
        $scope.btsDetails = result;
    });
});