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
                .when('/addPhoneNumber', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.signup"
                })
                .when('/modifyPhoneNumber/:number', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.modify"
                })
                .when('/changePassword', {
                    templateUrl: '/appViews/Manage/ChangePassword.html',
                    controller: "password.change"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function ($rootScope, authorizeService) {
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
            title: "本账号信息管理",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };
    });

app.controller("manage.current", function ($scope, authorizeService) {
    authorizeService.queryCurrentUserInfo().then(function (result) {
        $scope.currentUser = result;
    });
});
