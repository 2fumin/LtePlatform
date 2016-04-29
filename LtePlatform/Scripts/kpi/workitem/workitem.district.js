app.controller("workitem.district", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.page.title = "工单统计-" + $routeParams.district;

    $scope.updateWorkItemTable = function () {
        workitemService.queryTotalPagesByDistrict($scope.viewData.currentState.name, $scope.viewData.currentType.name,
            $routeParams.district).then(function (result) {
                $scope.totalItems = result;
                $scope.query();
            });
    };

    $scope.query = function () {
        workitemService.queryWithPagingByDistrict($scope.viewData.currentState.name, $scope.viewData.currentType.name,
            $routeParams.district, $scope.viewData.itemsPerPage.value, $scope.viewData.currentPage).then(function (result) {
                $scope.viewItems = result;
            });
    };

    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.query);
    };

    $scope.updateWorkItemTable();
});