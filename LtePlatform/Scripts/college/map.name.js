app.controller("map.name", function ($scope, $uibModal, $log, appRegionService, baiduMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    
    baiduMapService.initializeMap("all-map", 15);
});