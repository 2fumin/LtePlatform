app.controller("import.cells", function ($scope, $uibModal, $log, basicImportService) {
    $scope.editCell = function (item, index) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Parameters/Import/EditCellDialog.html',
            controller: 'import.cells.dialog',
            size: 'lg',
            resolve: {
                item: function () {
                    return item;
                }
            }
        });
        modalInstance.result.then(function (result) {
            $scope.postSingleCell(result, index);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.postSingleCell = function (item, index) {
        basicImportService.dumpOneCellExcel(item).then(function (result) {
            if (result) {
                $scope.importData.newCells.splice(index, 1);
                $scope.importData.updateMessages.push({
                    contents: "完成一个LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库！",
                    type: "success"
                });
            } else {
                $scope.importData.updateMessages.push({
                    contents: "LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库失败！",
                    type: "error"
                });
            }
        }, function (reason) {
            $scope.importData.updateMessages.push({
                contents: "LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库失败！原因是：" + reason,
                type: "error"
            });
        });

    };
});

app.controller('import.cells.dialog', function ($scope, $uibModalInstance, item) {
    $scope.item = item;
    $scope.dateOpened = false;

    $scope.ok = function () {
        $uibModalInstance.close($scope.item);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});