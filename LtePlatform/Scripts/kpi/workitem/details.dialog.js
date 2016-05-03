app.controller('workitem.details.dialog', function ($scope, $uibModalInstance, input, dialogTitle, workItemDialog, workitemService) {
    $scope.currentView = input;
    $scope.dialogTitle = dialogTitle;
    $scope.message = "";
    $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
    $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
    $scope.preventChangeParentView = true;

    $scope.ok = function () {
        $uibModalInstance.close($scope.message);
    };
    $scope.signIn = function() {
        workitemService.signIn($scope.currentView.serialNumber).then(function(result) {
            if (result) {
                $scope.currentView = result;
                $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
            }
        });
    };
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});