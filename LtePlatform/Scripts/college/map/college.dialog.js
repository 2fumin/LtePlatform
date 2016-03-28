app.controller('map.college.dialog', function ($scope, $uibModalInstance, college, dialogTitle) {
    $scope.college = college;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.college);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});