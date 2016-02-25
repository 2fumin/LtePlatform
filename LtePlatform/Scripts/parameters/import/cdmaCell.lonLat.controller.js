app.controller("cdmaCell.lonLat", function ($scope, neGeometryService) {
    $scope.newCdmaCellLonLatEdits
        = neGeometryService.queryCdmaCellLonLatEdits($scope.importData.newCdmaCells);

    $scope.postCdmaCellLonLat = function () {
        neGeometryService.mapLonLatEdits($scope.importData.newCdmaCells, $scope.newCdmaCellLonLatEdits);
    };

});