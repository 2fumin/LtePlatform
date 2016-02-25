app.controller("bts.lonLat", function ($scope, neGeometryService) {
    $scope.newBtsLonLatEdits
        = neGeometryService.queryBtsLonLatEdits($scope.importData.newBtss);

    $scope.postBtsLonLat = function () {
        neGeometryService.mapLonLatEdits($scope.importData.newBtss, $scope.newBtsLonLatEdits);
    };

});