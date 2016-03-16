app.controller("cell.info", function ($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.name + "-" + $stateParams.sectorId + "小区信息";
    networkElementService.queryCellInfo($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.cellInfo = result;
    });
});