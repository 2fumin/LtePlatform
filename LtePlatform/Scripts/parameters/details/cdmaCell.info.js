app.controller("cdmaCell.info", function($scope, $stateParams, networkElementService) {
    $scope.page.title = $stateParams.name + "-" + $stateParams.sectorId + "小区信息";
    $scope.isHuaweiCell = false;
    networkElementService.queryCdmaCellInfo($stateParams.btsId, $stateParams.sectorId).then(function(result) {
        $scope.cdmaCellDetails = result;
    });
});