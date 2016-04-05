app.controller("college.menu", function($scope, $stateParams) {
    $scope.collegeInfo.type = $stateParams.type || 'lte';
    $scope.collegeName = $stateParams.name;
});