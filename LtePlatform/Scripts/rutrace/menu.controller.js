app.config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Rutrace/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "Index.html",
                    controller: "rutrace.index"
                })
                .when('/trend', {
                    templateUrl: viewDir + "Trend.html",
                    controller: "rutrace.trend"
                })
                .when('/top', {
                    templateUrl: viewDir + "Top.html",
                    controller: "rutrace.top"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function ($rootScope) {
        var rootUrl = "/Rutrace#";
        $rootScope.menuItems = [
            {
                displayName: "指标总体情况",
                url: rootUrl + "/"
            }, {
                displayName: "指标变化趋势",
                url: rootUrl + "/trend"
            }
        ];
    });

app.controller("rutrace.root", function($scope) {
    $scope.page ={ title: "指标总体情况"};
});

app.controller("rutrace.index", function($scope) {
    $scope.page.title = "指标总体情况";
    $scope.statDate = {
        value: new Date() - 1,
        opened: false
    };
    $scope.city = {
        sekected: "佛山",
        options: ["佛山", "广州"]
    };
});

app.controller("rutrace.trend", function($scope) {
    $scope.page.title = "指标变化趋势";
})