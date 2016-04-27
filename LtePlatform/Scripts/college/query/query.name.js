app.controller("query.name", function ($scope, $stateParams, collegeService) {
    $scope.collegeInfo.url = $scope.rootPath + "query";
    $scope.collegeName = $stateParams.name;
    collegeService.queryENodebs($scope.collegeName).then(function (eNodebs) {
        $scope.eNodebList = eNodebs;
    });
    collegeService.queryCells($scope.collegeName).then(function (cells) {
        $scope.cellList = cells;
    });
    collegeService.queryBtss($scope.collegeName).then(function (btss) {
        $scope.btsList = btss;
    });
    collegeService.queryCdmaCells($scope.collegeName).then(function (cells) {
        $scope.cdmaCellList = cells;
    });
});