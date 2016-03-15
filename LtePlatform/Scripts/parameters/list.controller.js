app.controller("parameters.list", function ($scope, appRegionService) {
    $scope.page.title = "基础数据总揽";

    appRegionService.initializeCities().then(function(result) {
        $scope.city = result;
    });
});