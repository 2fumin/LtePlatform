app.controller('password.reset', function ($scope, $window, authorizeService, appUrlService) {
    $scope.signup = {
        userName: "",
        email: "",
        password: "",
        confirmPassword: "",
        code: appUrlService.parseQueryString($window.location.href).code
    };
    $scope.page = {
        messages: []
    };
    $scope.signupForm = function () {
        $scope.signupForm = function () {
            authorizeService.resetPassword($scope.signup).then(function (result) {
                $scope.page.messages.push({
                    contents: result.Message,
                    type: result.Type
                });
                if (result.Type === 'success') {
                    $window.location.href = "/Account/Login";
                }
            }, function (reason) {
                $scope.page.messages.push({
                    contents: reason,
                    type: 'warning'
                });
            });
        };
    };
    $scope.closeAlert = function (index) {
        $scope.page.messages.splice(index, 1);
    };
});