app.controller("neighbor.import", function ($scope, neighborImportService) {
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
    neighborImportService.queryDumpNeighbors().then(function(result) {
        $scope.progressInfo.totalDumpItems = result;
    });
    console.log($scope.progressInfo);
    $scope.huaweiInfo = {
        totalSuccessItems: 50,
        totalFailItems: 0,
        totalDumpItems: 100
    };
    $scope.dumpHuaweiItems = function() {
        console.log($scope.huaweiInfo);
    };

});