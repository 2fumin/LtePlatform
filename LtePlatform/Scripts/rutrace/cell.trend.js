app.controller("cell.trend", function ($scope, $routeParams, appKpiService, appFormatService) {
    $scope.page.title = "小区地理化分析" + "-" + $routeParams.name;
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
});