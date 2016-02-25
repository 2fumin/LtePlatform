app.controller("rutrace.map", function ($scope, $routeParams, $location, baiduMapService, networkElementService) {
    var cell = $scope.topStat.current;
    if ($routeParams.name !== cell.eNodebName + "-" + cell.sectorName) {
        if ($scope.topStat.cells[$routeParams.name] === undefined) {
            $location.path('/Rutrace#/top');
        }
        $scope.topStat.current = $scope.topStat.cells[$routeParams.name];
    }
    networkElementService.queryCellSectors([$scope.topStat.current]).then(function(result) {
        console.log(result);
    });
    $scope.page.title = "小区地理化分析"+ "-" + $routeParams.name;
    $scope.updateMenuItems("小区地理化分析", $scope.rootPath + "baidumap", $routeParams.name);
    baiduMapService.initializeMap("all-map", 12);
    $scope.toggleNeighbors = function() {
        console.log($("#neighbor-info-box").html());
    };
});