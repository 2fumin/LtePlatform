app.controller("rutrace.map", function ($scope, $routeParams, $location, baiduMapService, networkElementService) {
    var cell = $scope.topStat.current;
    $scope.currentSector = {};
    if ($routeParams.name !== cell.eNodebName + "-" + cell.sectorName) {
        if ($scope.topStat.cells[$routeParams.name] === undefined) {
            $location.path('/Rutrace#/top');
        }
        $scope.topStat.current = $scope.topStat.cells[$routeParams.name];
    }
    $scope.page.title = "小区地理化分析"+ "-" + $routeParams.name;
    $scope.updateMenuItems("小区地理化分析", $scope.rootPath + "baidumap", $routeParams.name);
    baiduMapService.initializeMap("all-map", 12);
    networkElementService.queryCellSectors([$scope.topStat.current]).then(function (result) {
        $scope.preciseSector = result[0];
        console.log($scope.preciseSector);
        var html = $("#neighbor-info-box").html();
        console.log(html);
        baiduMapService.addOneSector(baiduMapService.generateSector($scope.preciseSector), html);
    });
    $scope.toggleNeighbors = function() {
    };
});