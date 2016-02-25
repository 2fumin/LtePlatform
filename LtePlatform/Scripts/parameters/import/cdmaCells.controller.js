app.controller("import.cdmaCells", function($scope, $uibModal, $log, basicImportService) {
    $scope.editCdmaCell = function (item, index) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Parameters/Import/EditCdmaCellDialog.html',
            controller: 'import.cdmaCells.dialog',
            size: 'lg',
            resolve: {
                item: function () {
                    return item;
                }
            }
        });
        modalInstance.result.then(function (result) {
            $scope.postSingleCdmaCell(result, index);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.postSingleCdmaCell = function (item, index) {
        basicImportService.dumpOneCdmaCellExcel(item).then(function (result) {
            if (result) {
                $scope.importData.newCdmaCells.splice(index, 1);
                $scope.importData.updateMessages.push({
                    contents: "完成一个CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库！",
                    type: "success"
                });
            } else {
                $scope.importData.updateMessages.push({
                    contents: "CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库失败！",
                    type: "error"
                });
            }
        }, function (reason) {
            $scope.importData.updateMessages.push({
                contents: "CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库失败！原因是：" + reason,
                type: "error"
            });
        });

    };
});

app.controller('import.cdmaCells.dialog', function ($scope, $uibModalInstance, item) {
    $scope.item = item;
    $scope.dateOpened = false;

    $scope.ok = function () {
        $uibModalInstance.close($scope.item);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});