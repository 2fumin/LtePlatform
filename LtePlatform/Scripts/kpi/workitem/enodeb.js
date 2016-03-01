app.controller('workitem.eNodeb', function ($scope, networkElementService, $routeParams) {
    $scope.serialNumber = $routeParams.serialNumber;
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
});