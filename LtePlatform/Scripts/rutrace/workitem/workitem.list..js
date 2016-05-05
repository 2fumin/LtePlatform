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
                view.detailsPath = $scope.rootPath + "details/" + view.eNodebId + "/" + view.sectorId + "/" + view.eNodebName;
            });
            $scope.viewItems = views;
        });
    };
    $scope.showDetails = function (view) {
        workItemDialog.showDetails(view, $scope.queryWorkItems);
    };
    $scope.queryWorkItems();
});