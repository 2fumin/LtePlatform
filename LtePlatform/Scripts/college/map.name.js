app.controller("map.name", function ($scope, $uibModal, $stateParams, $log, appRegionService, baiduMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    $scope.collegeName = $stateParams.name;
    baiduMapService.initializeMap("all-map", 15);

    switch($stateParams.type) {
        case 'lte':
            break;
        case 'cdma':
            break;
        case 'lteDistribution':
            break;
        default:
            break;
    }
});