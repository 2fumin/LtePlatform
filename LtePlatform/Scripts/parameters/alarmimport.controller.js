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
    $scope.dumpItems = function() {
        alarmImportService.dumpSingleItem().then(function(result) {
            if (result) {
                $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
            } else {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            }
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.updateDumpHistory();

                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        }, function() {
            $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.updateDumpHistory();

                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        });
    };
    $scope.clearItems = function () {
        alarmImportService.clearImportItems().then(function () {
            $scope.progressInfo.totalDumpItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
            $scope.progressInfo.totalFailItems = 0;
        });
    };

    $scope.progressInfo = {
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };
    $scope.updateDumpHistory();
});