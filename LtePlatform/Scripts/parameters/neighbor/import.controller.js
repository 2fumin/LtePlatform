app.controller("neighbor.import", function ($scope, neighborImportService, flowImportService) {
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.progressInfo = {
        totalSuccessItems: 0,
        totalFailItems: 0,
        totalDumpItems: 0
    };
    $scope.clearItems = function () {
        neighborImportService.clearDumpNeighbors().then(function () {
            $scope.progressInfo.totalDumpItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
            $scope.progressInfo.totalFailItems = 0;
        });
    };
    $scope.dumpItems = function () {
        neighborImportService.dumpSingleItem().then(function (result) {
            neighborImportService.updateSuccessProgress(result, $scope.progressInfo, $scope.dumpNeighborItems);
        }, function () {
            neighborImportService.updateFailProgress($scope.progressInfo, $scope.dumpNeighborItems);
        });
    };

    $scope.huaweiInfo = {
        totalSuccessItems: 0,
        totalFailItems: 0,
        totalDumpItems: 0
    };
    $scope.clearHuaweiItems=function() {
        flowImportService.clearDumpHuaweis().then(function () {
            $scope.huaweiInfo.totalDumpItems = 0;
            $scope.huaweiInfo.totalSuccessItems = 0;
            $scope.huaweiInfo.totalFailItems = 0;
        });
    }
    $scope.dumpHuaweiItems = function() {
        flowImportService.dumpHuaweiItem().then(function (result) {
            neighborImportService.updateSuccessProgress(result, $scope.huaweiInfo, $scope.dumpHuaweiItems);
        }, function () {
            neighborImportService.updateFailProgress($scope.huaweiInfo, $scope.dumpHuaweiItems);
        });
    };
    $scope.zteInfo = {
        totalSuccessItems: 0,
        totalFailItems: 0,
        totalDumpItems: 0
    };
    $scope.clearZteItems = function () {
        flowImportService.clearDumpHuaweis().then(function () {
            $scope.zteInfo.totalDumpItems = 0;
            $scope.zteInfo.totalSuccessItems = 0;
            $scope.zteInfo.totalFailItems = 0;
        });
    }
    $scope.dumpZteItems = function () {
        flowImportService.dumpZteItem().then(function (result) {
            neighborImportService.updateSuccessProgress(result, $scope.zteInfo, $scope.dumpZteItems);
        }, function () {
            neighborImportService.updateFailProgress($scope.zteInfo, $scope.dumpZteItems);
        });
    };
    $scope.updateDumpHistory = function() {
        neighborImportService.queryDumpNeighbors().then(function(result) {
            $scope.progressInfo.totalDumpItems = result;
        });
        flowImportService.queryHuaweiFlows().then(function(result) {
            $scope.huaweiInfo.totalDumpItems = result;
        });
        flowImportService.queryZteFlows().then(function(result) {
            $scope.zteInfo.totalDumpItems = result;
        });
        flowImportService.queryDumpHistory($scope.beginDate.value, $scope.endDate.value).then(function (result) {
            $scope.dumpHistory = result;
        });
    };

    $scope.updateDumpHistory();
});