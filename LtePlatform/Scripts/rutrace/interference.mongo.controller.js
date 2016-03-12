
app.controller('interference.mongo', function ($scope, dumpProgress) {
    $scope.progressInfo = {
        dumpCells: [],
        totalSuccessItems: 0,
        totalFailItems: 0,
        cellInfo: ""
    };
    $scope.page.title = "从MongoDB导入";
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

    $scope.reset = function () {
        dumpProgress.resetProgress($scope.progressInfo, $scope.beginDate.value, $scope.endDate.value);
    };

    $scope.dump = function () {
        for (var i = 0; i < 16; i++) {
            dumpProgress.dumpMongo($scope.progressInfo, $scope.beginDate.value, $scope.endDate.value, i, 16);
        }
    };

    $scope.reset();
});