app.controller('cell.dialog', function ($scope, $uibModalInstance, collegeService, name, dialogTitle) {
    $scope.dialogTitle = dialogTitle;
    collegeService.queryCells(name).then(function(result) {
        $scope.cellList = result;
    });

    $scope.ok = function () {
        $uibModalInstance.close($scope.cellList);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});