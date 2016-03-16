app.controller("eNodeb.list", function($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.city + $stateParams.district + $stateParams.town + "LTE基站列表";
    networkElementService.queryENodebsInOneTown($stateParams.city, $stateParams.district, $stateParams.town).then(function(result) {
        $scope.eNodebList = result;
    });
});