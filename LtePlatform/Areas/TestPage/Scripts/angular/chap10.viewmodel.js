﻿app.config([
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
            .when('/accordion', {
                templateUrl: viewDir + "demo.accordion.html",
                controller: "AccordionDemoCtrl"
            })
            .when('/alert', {
                templateUrl: viewDir + "demo.alert.html",
                controller: "AlertDemoCtrl"
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
        displayName: "控件实例I",
        items: [
        {
            url: rootPath + "/accordion",
            displayName: "手风琴"
        }, {
            url: rootPath + "/alert",
            displayName: "警告框"
        }]
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