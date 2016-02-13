app.config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/appViews/Manage/CurrentUser.html',
                controller: "manage.current"
            })
            .when('/all', {
                templateUrl: '/appViews/Manage/AllUsers.html',
                controller: 'manage.all'
            })
            .when('/roles', {
                templateUrl: '/appViews/Manage/Roles.html',
                controller: 'manage.roles'
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);

app.controller("manage.root", function($scope) {
    $scope.panelRoot = "账号信息管理";
    $scope.dataModel = new AppDataModel();
});

app.controller("manage.current", function($scope) {
    $scope.panelTitle = "本用户权限";
});
