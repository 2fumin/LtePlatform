app.controller('password.forgot', function ($scope, authorizeService) {
    $scope.page = {
        messages: []
    };
    $scope.signup = {
        userName: "",
        email: ""
    };
    $scope.signupForm = function () {
        authorizeService.forgotPassword($scope.signup).then(function(result) {
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
    
    $scope.closeAlert = function (index) {
        $scope.page.messages.splice(index, 1);
    };
});