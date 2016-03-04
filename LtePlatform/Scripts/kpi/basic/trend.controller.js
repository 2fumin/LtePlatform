app.controller('kpi.trend', function ($scope, kpi2GService) {
    $scope.page.title = "指标变化趋势";
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
    $scope.configs = {};

    kpi2GService.queryKpiOptions().then(function (result) {
        $scope.kpi = {
            options: result,
            selected: result[0]
        };
    })
});