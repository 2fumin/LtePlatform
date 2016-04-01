app.controller('dump.cell.mongo', function ($scope, $uibModalInstance, dumpProgress, appFormatService, dialogTitle, eNodebId, sectorId, begin, end) {
    $scope.dialogTitle = dialogTitle;

    $scope.dateRecords = [];

    $scope.ok = function () {
        $uibModalInstance.close($scope.dateRecords);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.queryRecords = function() {
        angular.forEach($scope.dateRecords, function(record) {
            dumpProgress.queryExistedItems(eNodebId, sectorId, record.date).then(function(result) {
                var expectedDate = appFormatService.getDate(result.item1);
                for (var i = 0; i < $scope.dateRecords.length; i++) {
                    if ($scope.dateRecords[i].date === expectedDate) {
                        $scope.dateRecords[i].existedRecords = result.item2;
                        break;
                    }
                }
            });
        });
    };

    $scope.dateRecords = [];
    var startDate = new Date(begin);
    while (startDate < end) {
        var date = new Date(startDate);
        $scope.dateRecords.push({
            date: date,
            existedRecords: 0
        });
        startDate.setDate(date.getDate() + 1);
    }
    $scope.queryRecords();
});