app.controller("kpi.workitem.details", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.gobackPath = $scope.rootPath;
    $scope.gobackTitle = "返回总览";
    
    workitemService.querySingleItem($routeParams.number).then(function (result) {
        $scope.currentView = result;
        $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
        $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
    });

});

app.controller("kpi.workitem.details.district", function($scope, $routeParams, workitemService, workItemDialog) {
    $scope.gobackPath = $scope.rootPath + "stat/" + $routeParams.district;
    $scope.gobackTitle = "返回工单统计-" + $routeParams.district;

    workitemService.querySingleItem($routeParams.number).then(function (result) {
        $scope.currentView = result;
        $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
        $scope.feedbackInfos = workItemDialog.calculateFeedbackInfo($scope.currentView.feedbackContents);
    });
})