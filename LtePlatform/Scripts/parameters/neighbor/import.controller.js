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
            if (result) {
                $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
            } else {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            }
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        }, function () {
            $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        });
    };

    neighborImportService.queryDumpNeighbors().then(function(result) {
        $scope.progressInfo.totalDumpItems = result;
    });
});