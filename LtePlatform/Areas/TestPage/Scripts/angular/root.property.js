app.run(function ($rootScope) {
    // 使用.run访问$rootScope
    $rootScope.rootProperty = 'root scope';
});
app.controller("root.property", function ($scope) {
    $scope.pageTitle = "RootProperty";
});
app.controller('ParentController', function ($scope) {
    // 使用.controller访问`ng-controller`内部的属性
    // 在DOM忽略的$scope中，根据当前控制器进行推断
    $scope.parentProperty = 'parent scope';
});
app.controller('ChildController', function ($scope) {
    $scope.childProperty = 'child scope';
    // 同在DOM中一样，我们可以通过当前$scope直接访问原型中的任意属性
    $scope.fullSentenceFromChild = 'Same $scope: We can access: ' +
        $scope.rootProperty + ' and ' +
        $scope.parentProperty + ' and ' +
        $scope.childProperty;
});

app.directive('myDirective', function () {
    return {
        restrict: 'A',
        replace: true,
        scope: {
            myUrl: '@', //绑定策略
            myLinkText: '@' //绑定策略
        },
        template: '<a href="{{myUrl}}">' +
            '{{myLinkText}}</a>'
    };
});

app.directive('theirDirective', function () {
    return {
        restrict: 'A',
        replace: true,
        scope: {
            myUrl: '=someAttr', // 经过了修改
            myLinkText: '@'
        },
        template: '\
            <div>\
                <label>My Url Field:</label>\
                <input type="text"\
                    ng-model="myUrl" />\
                <a href="{{myUrl}}">{{myLinkText}}</a>\
            </div>'
    };
});

app.run(function ($rootScope, $timeout) {
    $timeout(function () {
        $rootScope.myHref = 'http://google.com';
    }, 2000);
});