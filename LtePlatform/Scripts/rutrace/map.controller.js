﻿app.controller("rutrace.map", function ($scope, baiduMapService) {
    baiduMapService.initializeMap("all-map", 12);
    $scope.toggleNeighbors = function() {
        console.log($("#neighbor-info-box").html());
    };
    
});