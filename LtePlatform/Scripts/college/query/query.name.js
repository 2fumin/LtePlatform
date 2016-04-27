app.controller("query.name", function ($scope, $stateParams, collegeService) {
    $scope.collegeInfo.url = $scope.rootPath + "query";
    $scope.collegeName = $stateParams.name;
    collegeService.queryENodebs($scope.collegeName).then(function (eNodebs) {
        $scope.eNodebList = eNodebs;
    });
});