app.controller("rutrace.workitems", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.page.title = "TOP小区工单历史";

    workitemService.queryByCellId($routeParams.cellId, $routeParams.sectorId).then(function(result) {
        $scope.viewItems = result;
        $scope.viewData.workItems = result;
        if (result.length > 0) {
            $scope.currentView = result[0];
            $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
        }
    });
});