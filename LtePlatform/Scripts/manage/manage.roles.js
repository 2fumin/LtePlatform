
app.controller('manage.roles', function ($scope, $http) {
    $scope.panelTitle = "角色权限管理";
    $scope.manageRoles = [];
    $scope.roleName = "";
    $scope.message = "";
    $scope.updateRoleList = function () {
        $http({
            method: 'GET',
            url: $scope.dataModel.applicationRolesUrl,
            headers: {
                'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
            }
        }).success(function (result) {
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
            data: {
                roleName: $scope.roleName,
                action: "create"
            }
        }).success(function(result) {
            $scope.updateRoleList();
            $scope.message = result;
        });
    };

    $scope.updateRoleList();
});