app.controller("neighbor.import", function ($scope, neighborImportService) {
    $scope.neighborInfo = {
        totalSuccessItems: 0,
        totalFailItems: 0,
        totalDumpItems: 0
    };
    $scope.huaweiInfo = {
        totalSuccessItems: 0,
        totalFailItems: 0,
        totalDumpItems: 0
    };
    $scope.progressInfo = $scope.huaweiInfo;

    $scope.clearNeighborItems = function () {
        neighborImportService.clearDumpNeighbors().then(function () {
            $scope.neighborInfo.totalDumpItems = 0;
            $scope.neighborInfo.totalSuccessItems = 0;
            $scope.neighborInfo.totalFailItems = 0;
        });
    };
    $scope.dumpNeighborItems = function () {
        neighborImportService.dumpSingleItem().then(function (result) {
            neighborImportService.updateSuccessProgress(result, $scope.progressInfo, $scope.dumpNeighborItems);
        }, function () {
            neighborImportService.updateFailProgress($scope.progressInfo, $scope.dumpNeighborItems);
        });
    };
    $scope.dumpHuaweiItems = function() {
        $scope.progressInfo = $scope.huaweiInfo;
    };
    $scope.clearHuaweiItems=function() {
        $scope.progressInfo = $scope.huaweiInfo;
    }

    neighborImportService.queryDumpNeighbors().then(function(result) {
        $scope.neighborInfo.totalDumpItems = result;
    });
});