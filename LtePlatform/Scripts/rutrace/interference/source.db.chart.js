app.controller('interference.source.db.chart', function($scope, $uibModalInstance, dialogTitle, eNodebId, sectorId, name,
    topPreciseService, neighborMongoService) {
    $scope.dialogTitle = dialogTitle;
    $scope.currentCellName =name + "-" + sectorId;
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
    $scope.showChart = function() {
        topPreciseService.queryInterferenceNeighbor($scope.beginDate.value, $scope.endDate.value,
            eNodebId, sectorId).then(function(result) {
                var pieOptions = topPreciseService.getInterferencePieOptions(result, $scope.currentCellName);
                $("#interference-over6db").highcharts(pieOptions.over6DbOption);
                $("#interference-over10db").highcharts(pieOptions.over10DbOption);
        });
    };

    $scope.ok = function () {
        $uibModalInstance.close('已处理');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.showChart();
});