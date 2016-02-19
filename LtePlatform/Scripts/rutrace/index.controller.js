
app.controller("rutrace.index", function ($scope, appRegionService, appKpiService, appFormatService) {
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
    $scope.showKpi = function() {
        appKpiService.getRecentPreciseRegionKpi($scope.city.selected, $scope.statDate.value)
            .then(function (result) {
                $scope.statDate.value = appFormatService.getDate(result.statDate);
                $scope.overallStat.districtStats = result.districtPreciseViews;
                $scope.overallStat.townStats = result.townPreciseViews;
            });
    };
    appRegionService.initializeCities()
        .then(function(result) {
            $scope.city = result;
        });

});
