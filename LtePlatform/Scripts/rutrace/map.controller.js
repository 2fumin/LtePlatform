app.controller("rutrace.map", function ($scope, baiduMapService) {
    baiduMapService.initializeMap("all-map", 12);
});