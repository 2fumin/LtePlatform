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
                .when('/chart', {
                    templateUrl: viewDir + "Chart.html",
                    controller: "rutrace.chart"
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
        $rootScope.rootPath = rootUrl + "/";
    });

app.controller("rutrace.root", function($scope) {
    $scope.page = { title: "指标总体情况" };
    $scope.overallStat = {
        currentDistrict: "",
        districtStats: [],
        townStats: [],
        cityStat: {}
    };
});

app.controller("rutrace.chart", function ($scope, $location, appKpiService) {
    if ($scope.overallStat.districtStats.length === 0) $location.path($scope.rootPath);
    $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.overallStat.districtStats, $scope.overallStat.townStats));
});

app.controller("rutrace.trend", function($scope) {
    $scope.page.title = "指标变化趋势";
})