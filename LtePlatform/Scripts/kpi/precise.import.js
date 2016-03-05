app.controller("precise.import", function($scope, kpiImportService) {
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

    kpiImportService.queryDumpHistroy($scope.beginDate.value, $scope.endDate.value).then(function(result) {
        $scope.dumpHistory = result;
    });

    kpiImportService.queryTotalDumpItems().then(function(result) {
        $scope.progressInfo.totalDumpItems = result;
    });

    kpiImportService.queryTownPreciseViews().then(function(result) {
        $scope.townPreciseViews = result;
    });
});