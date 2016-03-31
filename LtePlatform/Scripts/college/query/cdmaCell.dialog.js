app.controller('cdmaCell.dialog', function ($scope, $uibModalInstance, collegeService, name, dialogTitle) {
    $scope.dialogTitle = dialogTitle;
    collegeService.queryCdmaCells(name).then(function(result) {
        $scope.cdmaCellList = result;
    });

    $scope.ok = function () {
        $uibModalInstance.close($scope.cdmaCellList);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});