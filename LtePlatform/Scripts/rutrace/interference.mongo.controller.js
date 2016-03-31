
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
    $scope.currentPage = 1;

    $scope.reset = function () {
        dumpProgress.resetProgress($scope.beginDate.value, $scope.endDate.value).then(function(result) {
            $scope.progressInfo.dumpCells = result;
            $scope.progressInfo.totalFailItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
        });
    };

    $scope.dump = function () {
        for (var i = 0; i < 2; i++) {
            dumpProgress.dumpMongo($scope.progressInfo, $scope.beginDate.value, $scope.endDate.value, i, 2);
        }
    };

    $scope.reset();
});