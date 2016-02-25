app.controller("import.btss", function($scope, $uibModal, $log, appFormatService, basicImportService) {
    $scope.editBts = function(item, index) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Parameters/Import/EditBtsDialog.html',
            controller: 'import.btss.dialog',
            size: 'lg',
            resolve: {
                item: function() {
                    return item;
                }
            }
        });
        modalInstance.result.then(function(result) {
            $scope.postSingleBts(result, index);
        }, function() {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.postSingleBts = function(item, index) {
        basicImportService.dumpOneBtsExcel(item).then(function(result) {
            if (result) {
                $scope.importData.newBtss.splice(index, 1);
                $scope.importData.updateMessages.push({
                    contents: "完成一个CDMA基站'" + item.name + "'导入数据库！",
                    type: "success"
                });
            } else {
                $scope.importData.updateMessages.push({
                    contents: "CDMA基站'" + item.name + "'导入数据库失败！",
                    type: "error"
                });
            }
        }, function(reason) {
            $scope.importData.updateMessages.push({
                contents: "CDMA基站'" + item.name + "'导入数据库失败！原因是：" + reason,
                type: "error"
            });
        });
    };
});

app.controller('import.btss.dialog', function ($scope, $uibModalInstance, item) {
    $scope.item = item;
    $scope.dateOpened = false;

    $scope.ok = function () {
        $uibModalInstance.close($scope.item);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});