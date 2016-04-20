app.controller("home.workitem", function ($scope, workitemService) {
    workitemService.queryCurrentMonth().then(function (result) {
        $scope.totalItems = result.item1;
        $scope.finishedItems = result.item2;
        $scope.lateItems = result.item3;
    });
});