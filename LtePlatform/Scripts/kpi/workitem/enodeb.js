app.controller('workitem.eNodeb', function ($scope, $uibModal, $log, networkElementService, $routeParams, workitemService,
    workItemDialog) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByENodebId($routeParams.eNodebId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    $scope.feedback = function (view) {
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
            workitemService.feedback(output, view.serialNumber).then(function (result) {
                if (result)
                    $scope.queryWorkItems();
            });
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.showDetails=function(view) {
        workItemDialog.showDetails(view);
    };
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    $scope.queryWorkItems();
});