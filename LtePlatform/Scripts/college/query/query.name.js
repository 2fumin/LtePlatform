app.controller("query.name", function ($scope, $stateParams) {
    $scope.collegeInfo.url = $scope.rootPath + "query";
    $scope.collegeName = $stateParams.name;
});