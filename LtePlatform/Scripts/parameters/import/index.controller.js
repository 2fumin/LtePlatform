app.controller("import.index", function ($scope, basicImportService) {
    $scope.newENodebsImport = true;
    $scope.newCellsImport = true;
    $scope.newBtssImport = true;
    $scope.newCdmaCellsImport = true;

    $scope.postAllENodebs = function () {
        if ($scope.importData.newENodebs.length > 0) {
            basicImportService.dumpMultipleENodebExcels($scope.importData.newENodebs).then(function(result) {
                $scope.importData.updateMessages.push({
                    contents: "完成LTE基站导入" + result + "个",
                    type: 'success'
                });
                $scope.importData.newENodebs = [];
            });
        }
    };

    $scope.postAllCells = function () {
        if ($scope.importData.newCells.length > 0) {
            basicImportService.dumpMultipleCellExcels($scope.importData.newCells).then(function (result) {
                $scope.importData.updateMessages.push({
                    contents: "完成LTE小区导入" + result + "个",
                    type: 'success'
                });
                $scope.importData.newCells = [];
            });
        }
    };

});