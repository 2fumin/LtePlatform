
app.controller('manage.all', function ($scope, authorizeService) {
    $scope.page.title = "所有用户信息管理";
    $scope.manageUsers = [];
    authorizeService.queryAllUsers().then(function (result) {
        $scope.manageUsers = result;
        for (var i = 0; i < result.length; i++) {
            authorizeService.queryEmailConfirmed(result[i].userName).then(function (confirmed) {
                for (var j = 0; j < result.length; j++) {
                    if (result[j].userName === confirmed.Name) {
                        $scope.manageUsers[j].emailHasBeenConfirmed = confirmed.Result;
                        break;
                    }
                }
            });
        }
    });
});
