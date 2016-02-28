
app.controller('manage.all', function ($scope, authorizeService) {
    $scope.page.title = "所有用户信息管理";
    $scope.manageUsers = [];
    authorizeService.queryAllUsers().then(function (result) {
        $scope.manageUsers = result;
    });
});
