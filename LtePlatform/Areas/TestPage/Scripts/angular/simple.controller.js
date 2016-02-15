app.config([
    '$routeProvider', function ($routeProvider) {
        var viewDir = "/appViews/Test/Simple/";
        $routeProvider
            .when('/', {
                templateUrl: viewDir+"SimpleType.html",
                controller: "SimpleTypeController"
            })
            .when('/add', {
                templateUrl: viewDir+"Add.html",
                controller: "AddController"
            })
            .when('/clock', {
                templateUrl: viewDir+"Clock.html",
                controller: "ClockController"
            })
            .when('/interpolate', {
                templateUrl: viewDir+"Interpolate.html",
                controller: "InterpolateController"
            })
            .when('/parse', {
                templateUrl: viewDir + "Parse.html",
                controller: "ParseController"
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);

app.controller("simple.root", function ($scope) {
    $scope.pageTitle = "Simple";
    var rootPath = "/TestPage/AngularTest/Simple#";
    $scope.sectionItems = [
    {
        name: "Simple",
        displayName: "简单类型测试",
        url: rootPath + '/'
    }, {
        name: "Add",
        displayName: "简单加减法",
        url: rootPath + "/add"
    }, {
        name: "Clock",
        displayName: "时钟控制器",
        url: rootPath + "/clock"
    }, {
        name: "Interpolate",
        displayName: "插值字符串测试",
        url: rootPath + "/interpolate"
    }, {
        name: "Parse",
        displayName: "表达式解析测试",
        url: rootPath + "/parse"
    }];
});

var SimpleTypeController = function ($scope) {
    $scope.sectionTitle = "Simple";
    $scope.simpleA = 1;
    $scope.simpleB = 2;
}
app.controller("SimpleTypeController", SimpleTypeController);

var ClockController = function ($scope, $timeout) {
    $scope.sectionTitle = "Add";
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
    $scope.sectionTitle = "Clock";
    $scope.counter = 0;
    $scope.add = function (amount) { $scope.counter += amount; };
    $scope.subtract = function (amount) { $scope.counter -= amount; };
};
app.controller("AddController", AddController);

var ParseController = function ($scope, $parse) {
    $scope.sectionTitle = "Interpolate";
    $scope.$watch('expr', function (newVal, oldVal, scope) {
        if (newVal !== oldVal) {
            var parseFun = $parse(newVal);
            $scope.parsedValue = parseFun(scope);
        }
    });
};
app.controller("ParseController", ParseController);

var InterpolateController = function ($scope, $interpolate) {
    $scope.sectionTitle = "Parse";
    $scope.$watch('emailBody', function (body) {
        if (body) {
            var template = $interpolate(body);
            $scope.previewText = template({ to: $scope.to });
        }
    });
};
app.controller("InterpolateController", InterpolateController);