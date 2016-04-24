app.controller("cell.info", function ($scope, $stateParams, networkElementService, cellHuaweiMongoService, intraFreqHoService,
    interFreqHoService, neighborMongoService, cellPowerService) {
    $scope.page.title = $stateParams.name + "-" + $stateParams.sectorId + "小区信息";
    $scope.isHuaweiCell = false;
    networkElementService.queryCellInfo($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
    cellHuaweiMongoService.queryCellParameters($stateParams.eNodebId, $stateParams.sectorId).then(function(info) {
        $scope.cellMongo = info;
    });
    intraFreqHoService.queryCellParameters($stateParams.eNodebId, $stateParams.sectorId).then(function(result) {
        $scope.intraFreqHo = result;
    });
    interFreqHoService.queryCellParameters($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.interFreqHo = result;
    });
    cellPowerService.queryCellParameters($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.cellPower = result;
    });
    neighborMongoService.queryNeighbors($stateParams.eNodebId, $stateParams.sectorId).then(function (result) {
        $scope.mongoNeighbors = result;
    });
});