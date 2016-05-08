app.controller("workitem.city", function ($scope, workitemService, workItemDialog) {
    $scope.page.title = "精确覆盖优化工单一览";
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 100);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.queryWorkItems = function () {
        workitemService.queryPreciseDateSpan($scope.beginDate.value, $scope.endDate.value).then(function (views) {
            angular.forEach(views, function(view) {
                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
            });
            $scope.viewItems = views;
        });
    };
    $scope.showDetails = function (view) {
        workItemDialog.showDetails(view, $scope.queryWorkItems);
    };
    $scope.queryWorkItems();
});

app.controller("workitem.district", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.page.title = $routeParams.district + "精确覆盖优化工单一览";
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 100);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.queryWorkItems = function () {
        workitemService.queryPreciseDateSpanDistrict($scope.beginDate.value, $scope.endDate.value, $routeParams.district).then(function (views) {
            angular.forEach(views, function (view) {
                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
            });
            $scope.viewItems = views;
        });
    };
    $scope.showDetails = function (view) {
        workItemDialog.showDetails(view, $scope.queryWorkItems);
    };
    $scope.queryWorkItems();
});