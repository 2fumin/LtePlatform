app.directive("showcellinfo", function () {
    return {
        restrict: 'ECMA',
        template: '<button class="btn btn-sm btn-success" ng-click="show()">{{title}}</button>',
        scope: {
            show: '&show',
            title: '@'
        }
    }
});