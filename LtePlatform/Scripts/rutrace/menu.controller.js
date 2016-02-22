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
                .when('/trendchart', {
                    templateUrl: viewDir + "TrendChart.html",
                    controller: "rutrace.trendchart"
                })
                .when('/top', {
                    templateUrl: viewDir + "Top.html",
                    controller: "rutrace.top"
                })
                .when('/import', {
                    templateUrl: viewDir + "Import.html",
                    controller: "rutrace.import"
                })
                .when('/interference', {
                    templateUrl: viewDir + "Interference/Index.html",
                    controller: "rutrace.interference"
                })
                .when('/baidumap', {
                    templateUrl: viewDir + "Map/Index.html",
                    controller: "rutrace.map"
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
            }, {
                displayName: "TOP指标分析",
                url: rootUrl + "/top"
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
        cityStat: {},
        dateString: ""
    };
    $scope.trendStat = {
        mrStats: [],
        preciseStats: [],
        districts: [],
        districtStats: [],
        townStats: [],
        beginDateString: "",
        endDateString: ""
    };
    $scope.topStat = {
        current: {},
        interference: {},
        victims: {},
        coverages: {},
        updateInteferenceProgress: {},
        updateVictimProgress: {}
    };
});
