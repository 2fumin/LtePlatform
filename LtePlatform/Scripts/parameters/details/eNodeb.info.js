app.controller("eNodeb.info", function ($scope, $stateParams, networkElementService, intraFreqHoService) {
    $scope.page.title = $stateParams.name + "基础信息";
    networkElementService.queryENodebInfo($stateParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    networkElementService.queryCellViewsInOneENodeb($stateParams.eNodebId).then(function(result) {
        $scope.cellList = result;
    });
    intraFreqHoService.queryENodebParameters($stateParams.eNodebId).then(function(result) {
        $scope.intraFreqHo = result;
    });
});