app.controller("rutrace.map", function ($scope, $routeParams, baiduMapService) {
    $scope.page.title = "小区地理化分析"+ "-" + $routeParams.name;
    $scope.updateMenuItems("小区地理化分析", $scope.rootPath + "baidumap", $routeParams.name);
    baiduMapService.initializeMap("all-map", 12);
    $scope.toggleNeighbors = function() {
        console.log($("#neighbor-info-box").html());
    };
    
});