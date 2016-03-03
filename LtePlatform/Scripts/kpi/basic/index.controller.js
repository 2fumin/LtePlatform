app.controller("kpi.basic", function ($scope, appRegionService, appFormatService, kpi2GService) {
    $scope.page.title = "指标总体情况";
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    $scope.city = {
        selected: "",
        options: []
    };
    $scope.views = {
        options: ['主要', '2G', '3G'],
        selected: '主要'
    };
    $scope.showKpi = function () {
        kpi2GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function (result) {
            $scope.statDate.value = result.statDate;
            $scope.statList = result.statViews;
        });
    };
    appRegionService.initializeCities()
        .then(function (result) {
            $scope.city = result;
            $scope.showKpi();
        });
});