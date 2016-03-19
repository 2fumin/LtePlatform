app.controller('password.reset', function ($scope, $window, authorizeService, appUrlService) {
    $scope.signup = {
        userName: "",
        email: "",
        password: "",
        confirmPassword: "",
        code: appUrlService.parseQueryString($window.location.href).code
    };
    console.log($scope.signup.code);
    $scope.signupForm = function () {
        $scope.signupForm = function () {
            authorizeService.forgotPassword($scope.signup).then(function (result) {
                $scope.page.messages.push({
                    contents: result.Message,
                    type: result.Type
                });
            }, function (reason) {
                $scope.page.messages.push({
                    contents: reason,
                    type: 'warning'
                });
            });
        };
    };
});