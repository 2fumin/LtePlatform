app.controller('map.neighbor.dialog', function ($scope, $uibModalInstance, neighbor, dialogTitle) {
    $scope.neighbor = neighbor;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.neighbor);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});