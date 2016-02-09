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

app.controller('manage.all', function ($scope, $http) {
    $scope.panelTitle = "所有用户管理";
    $scope.manageUsers = [];
    $http({
        method: 'GET',
        url: $scope.dataModel.applicationUsersUrl,
        headers: {
            'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
        }
    }).success(function(result) {
        $scope.manageUsers = result;
    });
});

app.controller('manage.roles', function ($scope, $http) {
    $scope.panelTitle = "角色权限管理";
    $scope.manageRoles = [];
    $scope.roleName = "";
    $scope.updateRoleList = function () {
        $http({
            method: 'GET',
            url: $scope.dataModel.applicationRolesUrl,
            headers: {
                'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
            }
        }).success(function(result) {
            $scope.manageRoles = result;
            $scope.roleName = "New role " + result.length;
        });
    };
    $scope.addRole = function () {
        $http({
            method: 'GET',
            url: $scope.dataModel.applicationRolesUrl,
            headers: {
                'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
            },
            params: {
                'roleName': $scope.roleName
            }
        }).success(function (result) {
            if (result === true) {
                $scope.updateRoleList();
            }
        });
    };

    $scope.updateRoleList();
});