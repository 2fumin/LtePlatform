app.controller('kpi.topDrop2G', function ($scope, appRegionService) {
    $scope.page.title = "TOP掉话指标";
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
    $scope.showKpi = function() {

    };
    appRegionService.initializeCities().then(function (result) {
        $scope.city = result;
        $scope.showKpi();
    });
});