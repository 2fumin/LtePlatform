app.controller('map.bts.dialog', function ($scope, $uibModalInstance, bts, dialogTitle) {
    $scope.bts = bts;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.bts);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});