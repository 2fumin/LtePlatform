app.controller("alarm.import", function($scope, alarmImportService) {
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
    $scope.updateDumpHistory = function() {
        alarmImportService.queryDumpHistory($scope.beginDate.value, $scope.endDate.value).then(function(result) {
            $scope.dumpHistory = result;
        });
        alarmImportService.queryDumpItems().then(function(result) {
            $scope.progressInfo.totalDumpItems = result;
        });
    };

    $scope.progressInfo = {
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };
    $scope.updateDumpHistory();
});