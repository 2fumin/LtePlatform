app.controller('interference.source.dialog', function ($scope, $uibModalInstance, dialogTitle, eNodebId, sectorId) {
    $scope.dialogTitle = dialogTitle;
    $scope.eNodebId = eNodebId;
    $scope.sectorId = sectorId;

    $scope.ok = function () {
        $uibModalInstance.close('已处理');
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});