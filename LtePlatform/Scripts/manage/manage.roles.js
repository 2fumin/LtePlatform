
app.controller('manage.roles', function ($scope, authorizeService) {
    $scope.page.title = "所有角色管理";
    $scope.manageRoles = [];
    $scope.roleName = "";
    $scope.message = "";
    $scope.updateRoleList = function () {
        authorizeService.updateRoleList().then(function (result) {
            $scope.manageRoles = result;
            $scope.roleName = "New role " + result.length;
        });
    };
    $scope.addRole = function () {
        authorizeService.addRole($scope.roleName).then(function (result) {
            $scope.updateRoleList();
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
        }, function(reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'error'
            });
        });
    };
    $scope.deleteRole = function(name) {
        authorizeService.deleteRole(name).then(function (result) {
            $scope.updateRoleList();
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
        }, function (reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'error'
            });
        });
    };

    $scope.updateRoleList();
});