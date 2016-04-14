app.controller("map.name", function ($scope, $uibModal, $stateParams, $log, appRegionService, baiduMapService,
    collegeService, parametersMapService, parametersDialogService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";
    $scope.collegeName = $stateParams.name;
    baiduMapService.initializeMap("all-map", 15);

    switch($stateParams.type) {
        case 'lte':
            collegeService.queryENodebs($scope.collegeName).then(function(eNodebs) {
                parametersMapService.showENodebsElements(eNodebs, parametersDialogService.showENodebInfo);
            });
            collegeService.queryCells($scope.collegeName).then(function(cells) {
                parametersMapService.showCellSectors(cells, parametersDialogService.showCollegeCellInfo);
            });
            break;
        case 'cdma':
            collegeService.queryBtss($scope.collegeName).then(function (btss) {
                parametersMapService.showENodebsElements(btss, parametersDialogService.showENodebInfo);
            });
            collegeService.queryCdmaCells($scope.collegeName).then(function (cells) {
                parametersMapService.showCellSectors(cells, parametersDialogService.showCollegeCdmaCellInfo);
            });
            break;
        case 'lteDistribution':
            collegeService.queryLteDistributions($scope.collegeName).then(function(distributions) {
                parametersMapService.showENodebsElements(distributions, parametersDialogService.showDistributionInfo);
            });
            break;
        default:
            collegeService.queryCdmaDistributions($scope.collegeName).then(function (distributions) {
                parametersMapService.showENodebsElements(distributions, parametersDialogService.showDistributionInfo);
            });
            break;
    }
});