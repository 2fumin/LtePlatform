app.controller('interference.source.strength.chart', function($scope, $uibModalInstance, dialogTitle, eNodebId, sectorId, name,
    topPreciseService, neighborMongoService, networkElementService) {
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
                networkElementService.queryCellInfo(eNodebId, sectorId).then(function (info) {
                    topPreciseService.queryCellStastic(eNodebId, info.pci,
                        $scope.beginDate.value, $scope.endDate.value).then(function (stastic) {
                            var columnOptions = topPreciseService.getStrengthColumnOptions(result, stastic.mrCount,
                                $scope.currentCellName);
                            $("#strength-over6db").highcharts(columnOptions.over6DbOption);
                            $("#strength-over10db").highcharts(columnOptions.over10DbOption);
                        });
                });
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