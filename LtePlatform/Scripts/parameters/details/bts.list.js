app.controller("bts.list", function ($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.city + $stateParams.district + $stateParams.town + "CDMA基站列表";
    $scope.currentPage = 1;
    networkElementService.queryBtssInOneTown($stateParams.city, $stateParams.district, $stateParams.town).then(function (result) {
        $scope.btsList = result;
    });
});