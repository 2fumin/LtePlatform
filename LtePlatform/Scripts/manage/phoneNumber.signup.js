app.controller('phoneNumber.signup', function ($scope, $window) {
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            $window.location.href = "/Manage/AddPhoneNumber?number=" + $scope.signup.number;
        } else {
            $scope.page.messages.push({
                contents: '输入电话号码有误！请检查。',
                type: 'warning'
            });
        }
    };
})