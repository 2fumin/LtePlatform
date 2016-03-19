
app.controller("manage.current", function ($scope, authorizeService) {
    $scope.page.title = "本账号信息管理";
    authorizeService.queryCurrentUserInfo().then(function (result) {
        $scope.currentUser = result;
    });
    $scope.removePhoneNumber = function() {
        authorizeService.removePhoneNumber().then(function(result) {
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
            $scope.currentUser.phoneNumber = null;
        });
    };
    $scope.confirmEmail = function() {
        authorizeService.confirmEmail($scope.currentUser).then(function(result) {
            $scope.page.messages.push({
                contents: result.Message,
                type: result.Type
            });
        });
    };
});
