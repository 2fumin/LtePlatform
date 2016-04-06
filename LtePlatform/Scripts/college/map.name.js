app.controller("map.name", function ($scope, $uibModal, $stateParams, $log, appRegionService, baiduMapService,
    collegeService, parametersMapService, parametersDialogService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    $scope.collegeName = $stateParams.name;
    baiduMapService.initializeMap("all-map", 15);

    switch($stateParams.type) {
        case 'lte':
            collegeService.queryENodebs($scope.collegeName).then(function(eNodebs) {
                parametersMapService.showENodebsElements(eNodebs, parametersDialogService.showENodebInfo, parametersDialogService.showCellInfo);
            });
            break;
        case 'cdma':
            break;
        case 'lteDistribution':
            break;
        default:
            break;
    }
});