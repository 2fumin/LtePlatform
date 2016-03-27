app.controller("all.map", function ($scope, $uibModal, $log, appRegionService, baiduMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    baiduMapService.initializeMap("all-map", 11);
    baiduMapService.addCityBoundary("佛山");
});