app.controller("bts.list", function ($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.city + $stateParams.district + $stateParams.town + "CDMA基站列表";
    networkElementService.queryBtssInOneTown($stateParams.city, $stateParams.district, $stateParams.town).then(function (result) {
        $scope.btsList = result;
    });
});