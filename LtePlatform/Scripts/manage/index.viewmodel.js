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
            .when('/accounts', {
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

});

app.controller('manage.all', function($scope, $http) {
    $scope.manageUsers = [];
    $http({
        method: 'GET',
        url: $scope.dataModel.applicationUsersUrl,
        headers: {
            'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
        }
    }).success(function(result) {
        $scope.manageRoles = result;
    });
});

app.controller('manage.roles', function($scope, $http) {
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
            $scope.roleName = "New role " + data.length;
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