app.controller("home.kpi2G", function ($scope, appKpiService, appFormatService, kpi2GService) {
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    kpi2GService.queryDayStats($scope.city.selected || '佛山', $scope.statDate.value || new Date())
        .then(function (result) {
        $scope.statDate.value = appFormatService.getDate(result.statDate);
        var stat = result.statViews[result.statViews.length - 1];
        $scope.dropRate = stat.drop2GRate * 100;
        $scope.dropStar = appKpiService.calculateDropStar($scope.dropRate);
        $scope.connectionRate = stat.connectionRate * 100;
    });
});