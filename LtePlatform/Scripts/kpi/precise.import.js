app.controller("precise.import", function ($scope, preciseImportService) {
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
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };
    $scope.dumpHistory = [];
    $scope.townPreciseViews = [];

    $scope.clearItems = function() {
        preciseImportService.clearImportItems().then(function() {
            $scope.progressInfo.totalDumpItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
            $scope.progressInfo.totalFailItems = 0;
            $scope.townPreciseViews = [];
        });
    };

    preciseImportService.queryDumpHistroy($scope.beginDate.value, $scope.endDate.value).then(function (result) {
        $scope.dumpHistory = result;
    });
   
    preciseImportService.queryTotalDumpItems().then(function (result) {
        $scope.progressInfo.totalDumpItems = result;
    });

    preciseImportService.queryTownPreciseViews().then(function (result) {
        $scope.townPreciseViews = result;
    });
});