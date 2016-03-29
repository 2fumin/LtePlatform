app.controller('bts.dialog', function ($scope, $uibModalInstance, collegeService, name, dialogTitle) {
    $scope.dialogTitle = dialogTitle;
    collegeService.queryBtss(name).then(function(result) {
        $scope.btsList = result;
    });

    $scope.ok = function () {
        $uibModalInstance.close($scope.btsList);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});