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

app.controller('college.cdma.cell.dialog', function ($scope, $uibModalInstance, intraFreqHoService, interFreqHoService,
    neighbor, dialogTitle) {
    $scope.cell = neighbor;
    $scope.dialogTitle = dialogTitle;
    $scope.infoUrl = '/appViews/College/Table/CdmaCellBasicInfo.html';

    $scope.ok = function () {
        $uibModalInstance.close($scope.cell);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});