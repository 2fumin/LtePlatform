app.controller("rutrace.map", function ($scope, $routeParams, $location, baiduMapService, networkElementService) {
    var cell = $scope.topStat.current;
    $scope.preciseSector = {};
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
    });
    $scope.toggleNeighbors = function() {
        var html = $("#sector-info-box").html();
        baiduMapService.addOneSector(baiduMapService.generateSector($scope.preciseSector), html, "400px");
    };
});