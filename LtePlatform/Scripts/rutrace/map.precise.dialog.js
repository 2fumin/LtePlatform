app.controller('map.precise.dialog', function ($scope, $uibModalInstance, precise, dialogTitle) {
    $scope.preciseSector = precise;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.preciseSector);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});