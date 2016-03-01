app.controller('workitem.eNodeb', function ($scope, networkElementService, $routeParams) {
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
});