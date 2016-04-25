app.controller("kpi.workitem", function ($scope, $uibModal, $log, workitemService) {
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
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/WorkItem/FeedbackDialog.html',
            controller: 'workitem.feedback.dialog',
            size: 'lg',
            resolve: {
                dialogTitle: function () {
                    return view.serialNumber + "工单反馈";
                },
                input: function () {
                    return view;
                }
            }
        });

        modalInstance.result.then(function (output) {
            workitemService.feedback(output, view.serialNumber).then(function() {
                $scope.query();
            });
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    if ($scope.viewData.items.length === 0) {
        $scope.updateWorkItemTable();
    } else {
        $scope.viewItems = $scope.viewData.items;
    }
});

app.controller('workitem.feedback.dialog', function ($scope, $uibModalInstance, input, dialogTitle) {
    $scope.item = input;
    $scope.dialogTitle = dialogTitle;
    $scope.message = "";

    $scope.ok = function () {
        $uibModalInstance.close($scope.message);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});