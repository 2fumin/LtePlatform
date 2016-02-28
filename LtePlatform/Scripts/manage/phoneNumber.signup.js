app.controller('phoneNumber.signup',function($scope) {
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            console.log("The form is submit!");
        } else {
            $scope.signup_form.submitted = true;
        }
    };
})