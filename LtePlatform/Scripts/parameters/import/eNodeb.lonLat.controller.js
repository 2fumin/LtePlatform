app.controller("eNodeb.lonLat", function($scope, neGeometryService) {
    $scope.newENodebLonLatEdits
        = neGeometryService.queryENodebLonLatEdits($scope.importData.newENodebs);

    $scope.postENodebLonLat = function () {
        neGeometryService.mapLonLatEdits($scope.importData.newENodebs, $scope.newENodebLonLatEdits);
    };

});