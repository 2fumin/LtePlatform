app.controller('lte.distribution.dialog', function ($scope, $uibModalInstance, collegeService, name, dialogTitle) {
    $scope.dialogTitle = dialogTitle;
    collegeService.queryLteDistributions(name).then(function(result) {
        $scope.distributionList = result;
    });

    $scope.ok = function () {
        $uibModalInstance.close($scope.distributionList);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});