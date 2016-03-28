app.controller('eNodeb.dialog', function ($scope, $uibModalInstance, collegeService, name, dialogTitle) {
    $scope.dialogTitle = dialogTitle;
    collegeService.queryENodebs(name).then(function(result) {
        $scope.eNodebList = result;
    });

    $scope.ok = function () {
        $uibModalInstance.close($scope.eNodebList);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});