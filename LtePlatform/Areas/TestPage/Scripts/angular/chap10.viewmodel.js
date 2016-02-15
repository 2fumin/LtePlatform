app.config([
    '$routeProvider', function ($routeProvider) {
        var viewDir = "/appViews/Test/Chapter10/";
        $routeProvider
            .when('/', {
                templateUrl: viewDir + "Links.html",
                controller: "LinksController"
            })
            .when('/date', {
                templateUrl: viewDir + "DateTimeTest.html",
                controller: "DateController"
            })
            .when('/clock', {
                templateUrl: viewDir + "Clock.html",
                controller: "ClockController"
            })
            .when('/interpolate', {
                templateUrl: viewDir + "Interpolate.html",
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

app.controller("chap10.root", function ($scope) {
    $scope.pageTitle = "Chapter10Ari";
    var rootPath = "/TestPage/AngularTest/Chapter10Ari#";
    $scope.sectionItems = [
    {
        name: "Links",
        displayName: "指令测试",
        url: rootPath + '/'
    }, {
        name: "Date",
        displayName: "日期控件",
        url: rootPath + "/date"
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

app.controller("LinksController", function($scope) {
    $scope.sectionTitle = "Links";
});
