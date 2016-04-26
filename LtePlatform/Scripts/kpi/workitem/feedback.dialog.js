
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