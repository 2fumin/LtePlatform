app.controller('map.distribution.dialog', function ($scope, $uibModalInstance, distribution, dialogTitle) {
    $scope.distribution = distribution;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.distribution);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});