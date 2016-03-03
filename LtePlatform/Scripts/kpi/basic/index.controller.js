app.controller("kpi.basic", function ($scope, appRegionService, appFormatService) {
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

    };
    appRegionService.initializeCities()
        .then(function (result) {
            $scope.city = result;
            $scope.showKpi();
        });
});