app.controller("eNodeb.info", function ($scope, $stateParams, networkElementService, cellHuaweiMongoService, 
    alarmImportService, intraFreqHoService, interFreqHoService, appRegionService) {
    $scope.page.title = $stateParams.name + "LTE基础信息";

    appRegionService.queryENodebTown($stateParams.eNodebId).then(function(result) {
        $scope.city = result.item1;
        $scope.district = result.item2;
        $scope.town = result.item3;
    });

    //查询基站基本信息
    networkElementService.queryENodebInfo($stateParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
        if (result.factory === '华为') {
            cellHuaweiMongoService.queryLocalCellDef(result.eNodebId).then(function(cellDef) {
                alarmImportService.updateHuaweiAlarmInfos(cellDef).then(function() {});
            });
        }
    });

    //查询该基站下带的小区列表
    networkElementService.queryCellViewsInOneENodeb($stateParams.eNodebId).then(function(result) {
        $scope.cellList = result;
    });

    //查询基站同频切换参数
    intraFreqHoService.queryENodebParameters($stateParams.eNodebId).then(function(result) {
        $scope.intraFreqHo = result;
    });

    //查询基站异频切换参数
    interFreqHoService.queryENodebParameters($stateParams.eNodebId).then(function (result) {
        $scope.interFreqHo = result;
    });
});