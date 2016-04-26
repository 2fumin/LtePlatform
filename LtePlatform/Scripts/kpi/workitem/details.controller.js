app.controller("kpi.workitem.details", function ($scope, $http, $routeParams, workitemService,
    workItemDialog) {
    $scope.detailsView = "none";
    
    $scope.dialogTitle = "工单网元信息";

    if ($scope.currentView === undefined) {
        workitemService.querySingleItem($routeParams.number).then(function(result) {
            $scope.currentView = result;
            $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
        });
    } else {
        for (var i = 0; i < $scope.viewData.items.length; i++) {
            if ($scope.viewData.items[i].serialNumber === $routeParams.number) {
                $scope.currentView = $scope.viewData.items[i];
                $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
                $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
                break;
            }
        }
    }

});