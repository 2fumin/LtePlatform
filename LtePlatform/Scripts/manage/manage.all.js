
app.controller('manage.all', function ($scope, $http) {
    $scope.panelTitle = "所有用户管理";
    $scope.manageUsers = [];
    $http({
        method: 'GET',
        url: $scope.dataModel.applicationUsersUrl,
        headers: {
            'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
        }
    }).success(function (result) {
        $scope.manageUsers = result;
    });
});
