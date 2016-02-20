app.controller("rutrace.top", function ($scope, $http, appUrlService) {
    $scope.page.title = "TOP指标分析";
    $scope.topCells = [];

    $scope.query = function () {
        $scope.topCells = [];
        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('PreciseStat'),
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value,
                'topCount': 20,
                'orderSelection': "按照精确覆盖率升序"
            }
        }).success(function (result) {
            $scope.topCells = result;
        });
    };
    $scope.monitorAll = function () {
        for (var i = 0; i < $scope.topCells.length; i++) {
            var cell = $scope.topCells[i];
            if (cell.isMonitored === false) {
                $scope.addMonitor(cell);
            }
        }
    };

    $scope.query();
});