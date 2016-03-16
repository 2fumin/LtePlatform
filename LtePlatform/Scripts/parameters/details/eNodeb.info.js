app.controller("eNodeb.info", function ($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.name + "基础信息";
    networkElementService.queryENodebInfo($stateParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    networkElementService.queryCellInfosInOneENodeb($stateParams.eNodebId).then(function(result) {
        $scope.cellList = result;
    });
});