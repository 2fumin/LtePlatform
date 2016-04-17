app.controller("home.kpi2G", function ($scope, appKpiService, appFormatService, kpi2GService) {
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    kpi2GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function (result) {
        $scope.statDate.value = appFormatService.getDate(result.statDate);
        var stat = result.statViews[result.statViews.length - 1];
        $scope.dropRate = stat.drop2GRate * 100;
        $scope.connectionRate = stat.connectionRate * 100;
    });
});