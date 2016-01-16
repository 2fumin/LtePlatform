app.controller('SomeController', function ($scope) {
    // 反模式，裸值
    $scope.someBareValue = 'hello computer';
    $scope.someModel = {
        someValue: 'hello computer 2'
    }
    // 设置$scope本身的操作，这样没问题
    $scope.someAction = function () {
        // 在SomeController和ChildController内部设置{{ someBareValue }}
        $scope.someBareValue = 'hello human, from parent';
        $scope.someModel.someValue = 'hello human, from parent 2';
    };
})
.controller('ChildController', function ($scope) {
    $scope.childAction = function () {
        // 在ChildController内部设置{{ someBareValue }}
        $scope.someBareValue = 'hello human, from child';
        $scope.someModel.someValue = 'hello human, from child 2';
    };
});