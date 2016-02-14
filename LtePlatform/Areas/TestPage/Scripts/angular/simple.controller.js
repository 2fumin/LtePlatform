app.controller("simple.root", function($scope) {
    $scope.pageTitle = "Simple";
});

var SimpleTypeController = function ($scope) {
    $scope.simpleA = 1;
    $scope.simpleB = 2;
}
app.controller("SimpleTypeController", SimpleTypeController);

var ClockController = function ($scope, $timeout) {
    var updateClock = function () {
        $scope.clock = new Date();
        $timeout(function () {
            updateClock();
        }, 1000);
    };
    updateClock();
};
app.controller("ClockController", ClockController);

var AddController = function ($scope) {
    $scope.counter = 0;
    $scope.add = function (amount) { $scope.counter += amount; };
    $scope.subtract = function (amount) { $scope.counter -= amount; };
};
app.controller("AddController", AddController);

var ParseController = function ($scope, $parse) {
    $scope.$watch('expr', function (newVal, oldVal, scope) {
        if (newVal !== oldVal) {
            var parseFun = $parse(newVal);
            $scope.parsedValue = parseFun(scope);
        }
    });
};
app.controller("ParseController", ParseController);

var InterpolateController = function ($scope, $interpolate) {
    // 设置监听
    $scope.$watch('emailBody', function (body) {
        if (body) {
            var template = $interpolate(body);
            $scope.previewText = template({ to: $scope.to });
        }
    });
};
app.controller("InterpolateController", InterpolateController);