app.controller('OtherController', function ($scope) {
    // 最佳实践，永远使用一个模式
    $scope.someModel = {
        someValue: 'hello computer'
    }
    $scope.someAction = function () {
        $scope.someModel.someValue = 'hello human, from parent';
    };
})
.controller('OtherChildController', function ($scope) {
    $scope.childAction = function () {
        $scope.someModel.someValue = 'hello human, from child';
    };
});