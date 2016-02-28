app.config([
        '$routeProvider', function($routeProvider) {
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
                .when('/roleUser/:name', {
                    templateUrl: '/appViews/Manage/RoleUser.html',
                    controller: 'manage.roleUser'
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Manage#";
        $rootScope.menuItems = [
            {
                displayName: "本账号信息管理",
                url: rootUrl + "/"
            }, {
                displayName: "所有用户信息管理",
                url: rootUrl + "/all"
            }, {
                displayName: "所有角色管理",
                url: rootUrl + "/roles"
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "本账号信息管理"
        };
    });

app.controller("manage.current", function($scope, $http) {
    $http.get("/api/CurrentUser", null).success(function(result) {
        $scope.currentUser = result;
    });
});
