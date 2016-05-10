app.controller('interference.source.mod.chart', function($scope, $uibModalInstance, dialogTitle, eNodebId, sectorId, name,
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
                $("#interference-mod3").highcharts(pieOptions.mod3Option);
                $("#interference-mod6").highcharts(pieOptions.mod6Option);
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