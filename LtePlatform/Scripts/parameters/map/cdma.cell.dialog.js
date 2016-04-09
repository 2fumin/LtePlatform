app.controller('map.cdma.cell.dialog', function ($scope, $uibModalInstance, intraFreqHoService, interFreqHoService,
    neighbor, dialogTitle) {
    $scope.neighbor = neighbor;
    $scope.dialogTitle = dialogTitle;

    $scope.ok = function () {
        $uibModalInstance.close($scope.neighbor);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});