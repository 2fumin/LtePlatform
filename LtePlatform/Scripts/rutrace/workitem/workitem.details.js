app.controller("workitem.details", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.page.title = "工单编号" + $routeParams.number + "信息";
    $scope.queryWorkItems = function () {
        workitemService.querySingleItem($routeParams.number).then(function (result) {
            $scope.currentView = result;
            $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    $scope.queryWorkItems();
});