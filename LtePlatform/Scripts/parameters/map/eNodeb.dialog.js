app.controller('map.eNodeb.dialog', function ($scope, $uibModalInstance, eNodeb, dialogTitle) {
    $scope.eNodeb = eNodeb;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.eNodeb);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});