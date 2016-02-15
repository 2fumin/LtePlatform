
app.controller('interference.mongo', function ($scope, $http, dumpProgress) {
    $scope.dataModel = new AppDataModel();
    $scope.progressInfo = {
        dumpCells: [],
        totalSuccessItems: 0,
        totalFailItems: 0,
        cellInfo: ""
    };
    $scope.panelTitle = "从MongoDB导入";

    $scope.reset = function () {
        $http({
            method: 'GET',
            url: $scope.dataModel.dumpInterferenceUrl,
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value
            }
        }).success(function (result) {
            $scope.progressInfo.dumpCells = result;
            $scope.progressInfo.totalFailItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
        });
    };

    $scope.dump = function () {
        for (var i = 0; i < 16; i++) {
            dumpProgress.dumpMongo($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo, $scope.beginDate.value, $scope.endDate.value, i, 16);
        }
    };

    $scope.reset();
});