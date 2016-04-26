app.controller("kpi.workitem", function ($scope, workitemService, workItemDialog) {
    $scope.page.title = "工单总览";
    
    $scope.updateWorkItemTable = function() {
        workitemService.queryTotalPages($scope.viewData.currentState.name, $scope.viewData.currentType.name,
            $scope.viewData.itemsPerPage.value).then(function (result) {
                $scope.viewData.totalItems = result;
            $scope.query();
        });
    };

    $scope.query = function () {
        workitemService.queryWithPaging($scope.viewData.currentState.name, $scope.viewData.currentType.name,
            $scope.viewData.itemsPerPage.value, $scope.viewData.currentPage).then(function (result) {
                $scope.viewData.items = result;
                $scope.viewItems = $scope.viewData.items;
        });
    };

    $scope.updateSectorIds = function() {
        workitemService.updateSectorIds().then(function (result) {
            $scope.page.messages.push({
                contents: "一共更新扇区编号：" + result + "条",
                type: "success"
            });
        });
    };

    $scope.feedback = function(view) {
        workItemDialog.feedback(view, $scope.query);
    };

    if ($scope.viewData.items.length === 0) {
        $scope.updateWorkItemTable();
    } else {
        $scope.viewItems = $scope.viewData.items;
    }
});
