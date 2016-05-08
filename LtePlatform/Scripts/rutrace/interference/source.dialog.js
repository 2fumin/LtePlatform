app.controller('interference.source.dialog', function ($scope, $uibModalInstance, dialogTitle, eNodebId, sectorId,
    topPreciseService, neighborMongoService) {
    $scope.dialogTitle = dialogTitle;
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
    var options = [
        {
            name: "模3干扰数",
            value: "mod3Interferences"
        }, {
            name: "模6干扰数",
            value: "mod6Interferences"
        }, {
            name: "6dB干扰数",
            value: "overInterferences6Db"
        }, {
            name: "10dB干扰数",
            value: "overInterferences10Db"
        }, {
            name: "总干扰水平",
            value: "interferenceLevel"
        }
    ];
    $scope.orderPolicy = {
        options: options,
        selected: options[4].value
    };

    $scope.showInterference = function () {
        $scope.interferenceCells = [];

        topPreciseService.queryInterferenceNeighbor($scope.beginDate.value, $scope.endDate.value,
            eNodebId, sectorId).then(function (result) {
                angular.forEach(result, function (cell) {
                    for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                        var neighbor = $scope.mongoNeighbors[i];
                        if (neighbor.neighborPci === cell.destPci) {
                            cell.isMongoNeighbor = true;
                            break;
                        }
                    }
                });
                $scope.interferenceCells = result;
            });
    };

    $scope.ok = function () {
        $uibModalInstance.close('已处理');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    neighborMongoService.queryNeighbors(eNodebId, sectorId).then(function (result) {
        $scope.mongoNeighbors = result;
        $scope.showInterference();
    });
});