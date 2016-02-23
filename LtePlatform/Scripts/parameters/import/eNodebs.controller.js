app.controller("import.eNodebs", function ($scope, $uibModal, $log, appFormatService, basicImportService) {
    $scope.editENodeb = function (item, index) {
        if (!item.dateTransefered) {
            item.openDate = appFormatService.getDate(item.openDate);
            item.dateTransefered = true;
        }
        
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Parameters/Import/EditENodebDialog.html',
            controller: 'import.eNodebs.dialog',
            size: 'lg',
            resolve: {
                item: function() {
                    return item;
                }
            }
        });
        modalInstance.result.then(function(result) {
            $scope.postSingleENodeb(result, index);
        }, function() {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
    $scope.postSingleENodeb = function(item, index) {
        basicImportService.dumpOneENodebExcel(item).then(function(result) {
            if (result) {
                $scope.importData.newENodebs.splice(index, 1);
                $scope.importData.updateMessages.push({
                    contents: "完成一个LTE基站'" + item.name + "'导入数据库！",
                    type: "success"
                });
            } else {
                $scope.importData.updateMessages.push({
                    contents: "LTE基站'" + item.name + "'导入数据库失败！",
                    type: "error"
                });
            }
        }, function(reason) {
            $scope.importData.updateMessages.push({
                contents: "LTE基站'" + item.name + "'导入数据库失败！原因是：" + reason,
                type: "error"
            });
        });

    };
});

app.controller('import.eNodebs.dialog', function($scope, $uibModalInstance, item) {
    $scope.item = item;
    $scope.dateOpened = false;

    $scope.ok = function () {
        $uibModalInstance.close($scope.item);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});