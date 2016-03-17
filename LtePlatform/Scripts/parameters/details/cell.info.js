app.controller("cell.info", function ($scope, $stateParams, networkElementService, cellHuaweiMongoService) {
    $scope.page.title = $stateParams.name + "-" + $stateParams.sectorId + "小区信息";
    $scope.isHuaweiCell = false;
    networkElementService.queryCellInfo($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
    networkElementService.queryENodebInfo($stateParams.eNodebId).then(function(result) {
        if (result.factory === '华为') {
            $scope.isHuaweiCell = true;
            cellHuaweiMongoService.queryCellParameters($stateParams.eNodebId, $stateParams.sectorId).then(function(info) {
                $scope.cellMongo = info;
                $scope.cellMongo.cellSpecificOffsetdB = cellHuaweiMongoService.cellSpecificOffsetDict[info.cellSpecificOffset];
                $scope.cellMongo.qoffsetFreqdB = cellHuaweiMongoService.cellSpecificOffsetDict[info.qoffsetFreq];
            });
        }
    });
});