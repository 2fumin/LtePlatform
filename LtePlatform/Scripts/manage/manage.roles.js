
app.controller('manage.roles', function ($scope, authorizeService) {
    $scope.page.title = "所有角色管理";
    $scope.manageRoles = [];
    $scope.inputRole = {
        name: ""
    };
    $scope.updateRoleList = function () {
        authorizeService.updateRoleList().then(function (result) {
            $scope.manageRoles = result;
            $scope.inputRole.name = "New role " + result.length;
        });
    };
    $scope.addRole = function () {
        authorizeService.addRole($scope.inputRole.name).then(function (result) {
            $scope.updateRoleList();
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
        }, function(reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'warning'
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
                type: 'warning'
            });
        });
    };

    $scope.updateRoleList();
});