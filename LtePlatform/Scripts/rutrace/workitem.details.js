app.controller("workitem.details", function($scope, $routeParams, workitemService, workItemDialog) {
    if ($scope.viewData.workItems === undefined || $scope.viewData.workItems.length === 0) {
        workitemService.querySingleItem($routeParams.number).then(function (result) {
            $scope.currentView = result;
            $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
        });
    } else {
        $scope.viewItems = $scope.viewData.workItems;
        for (var i = 0; i < $scope.viewData.workItems.length; i++) {
            if ($scope.viewData.workItems[i].serialNumber === $routeParams.number) {
                $scope.currentView = $scope.viewData.workItems[i];
                $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
                $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
                break;
            }
        }
    }
});