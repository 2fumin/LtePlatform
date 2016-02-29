app.controller('phoneNumber.signup', function ($scope, authorizeService, $window) {
    $scope.action = "添加";
    $scope.signup = {
        phoneNumber: "",
        code: "123"
    };
    $scope.verify = false;
    $scope.signupForm = function() {
        if ($scope.signup_form.$valid) {
            if ($scope.verify) {
                authorizeService.verifyPhoneNumber($scope.signup).then(function (result) {
                    $scope.page.messages.push({
                        contents: result,
                        type: 'success'
                    });
                    $window.location.href = $scope.rootPath;
                });
            } else {
                authorizeService.addPhoneNumber($scope.signup).then(function(result) {
                    $scope.signup = {
                        code: result.Code,
                        phoneNumber: result.PhoneNumber
                    };
                    $scope.verify = true;
                });
            }
        } else {
            $scope.page.messages.push({
                contents: '输入电话号码有误！请检查。',
                type: 'warning'
            });
        }
    };
});

app.controller('phoneNumber.modify', function ($scope, authorizeService, $routeParams, $window) {
    $scope.action = "修改";
    $scope.signup = {
        phoneNumber: $routeParams.number,
        code: "123"
    };
    $scope.verify = false;
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            if ($scope.verify) {
                authorizeService.verifyPhoneNumber($scope.signup).then(function (result) {
                    $scope.page.messages.push({
                        contents: result,
                        type: 'success'
                    });
                    $window.location.href = $scope.rootPath;
                });
            } else {
                authorizeService.addPhoneNumber($scope.signup).then(function (result) {
                    $scope.signup = {
                        code: result.Code,
                        phoneNumber: result.PhoneNumber
                    };
                    $scope.verify = true;
                });
            }
        } else {
            $scope.page.messages.push({
                contents: '输入电话号码有误！请检查。',
                type: 'warning'
            });
        }
    };
});