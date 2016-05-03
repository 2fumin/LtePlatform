app.controller("evaluation.home", function ($scope, $http, baiduMapService, geometryService, parametersMapService, appUrlService) {
    geometryService.queryWandonglouyu().then(function(buildings) {
        baiduMapService.initializeMap("map", 12);
        parametersMapService.showPhpElements(buildings);
    });
});