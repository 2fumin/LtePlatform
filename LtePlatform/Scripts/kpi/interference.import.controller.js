
app.controller("interference.import", function ($scope, $http, dumpProgress) {
    $scope.progressInfo = {
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };
    $scope.panelTitle = "从数据文件导入";

    $scope.clearItems = function () {
        dumpProgress.clear($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo);
    };

    $scope.dumpItems = function () {
        dumpProgress.dump($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo);
    };

    $http.get($scope.dataModel.dumpInterferenceUrl).success(function (result) {
        $scope.progressInfo.totalDumpItems = result;
    });
});
