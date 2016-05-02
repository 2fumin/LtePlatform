app.controller('workitem.details.dialog', function ($scope, $uibModalInstance, input, dialogTitle, workItemDialog) {
    $scope.currentView = input;
    $scope.dialogTitle = dialogTitle;
    $scope.message = "";
    $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
    $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
    $scope.preventChangeParentView = true;

    $scope.ok = function () {
        $uibModalInstance.close($scope.message);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});