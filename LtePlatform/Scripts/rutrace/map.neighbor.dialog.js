app.controller('map.neighbor.dialog', function ($scope, $uibModalInstance, neighbor, dialogTitle) {
    $scope.neighbor = neighbor;
    $scope.dialogTitle = dialogTitle;
    $scope.parameter = {
        options: ['基本参数', '同频切换', '异频切换'],
        selected: '基本参数'
    };

    $scope.ok = function () {
        $uibModalInstance.close($scope.neighbor);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});