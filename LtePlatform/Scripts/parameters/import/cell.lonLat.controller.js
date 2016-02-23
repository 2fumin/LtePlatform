app.controller("cell.lonLat", function ($scope, neGeometryService) {
    $scope.newCellLonLatEdits
        = neGeometryService.queryCellLonLatEdits($scope.importData.newCells);

    $scope.postCellLonLat = function () {
        neGeometryService.mapLonLatEdits($scope.importData.newCells, $scope.newCellLonLatEdits);
    };

});