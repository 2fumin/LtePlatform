app.controller('password.change', function ($scope, authorizeService) {
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            authorizeService.changePassword($scope.signup).then(function(result) {
                $scope.page.messages.push({
                    contents: result,
                    type: 'success'
                }, function(reason) {
                    $scope.page.messages.push({
                        contents: reason,
                        type: 'warning'
                    });
                });
            });
        } else {
            $scope.page.messages.push({
                contents: '输入密码有误！请检查。',
                type: 'warning'
            });
        }
    };
});