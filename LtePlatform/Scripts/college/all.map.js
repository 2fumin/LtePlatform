app.controller("all.map", function ($scope, $uibModal, $log, appRegionService, baiduMapService) {
    baiduMapService.initializeMap("all-map", 11);
    baiduMapService.addCityBoundary("佛山");
});