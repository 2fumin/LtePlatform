
app.controller("rutrace.index", function ($scope, appRegionService) {
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
        console.log($scope.city);
    };
    appRegionService.initializeCities()
        .then(function(result) {
            $scope.city = result;
        });

});
