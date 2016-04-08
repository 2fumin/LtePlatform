app.controller("bts.info", function ($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.name + "CDMA基础信息";
    networkElementService.queryBtsInfo($stateParams.btsId).then(function (result) {
        $scope.btsDetails = result;
    });
    networkElementService.queryCdmaCellViews($stateParams.name).then(function (result) {
        $scope.cdmaCellList = result;
    });
});