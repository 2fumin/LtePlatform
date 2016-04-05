app.controller("map.name", function ($scope, $uibModal, $stateParams, $log, appRegionService, baiduMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    $scope.collegeName = $stateParams.name;
    baiduMapService.initializeMap("all-map", 15);
});