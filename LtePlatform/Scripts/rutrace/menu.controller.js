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
});

app.controller("rutrace.chart", function ($scope, $location, appKpiService) {
    if ($scope.overallStat.districtStats.length === 0) $location.path($scope.rootPath);

    $scope.showCharts = function() {
        $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.overallStat.districtStats,
            $scope.overallStat.townStats));
        $("#precise").highcharts(appKpiService.getPreciseRateOptions($scope.overallStat.districtStats,
            $scope.overallStat.townStats));
    };
});

app.controller("rutrace.trendchart", function ($scope, $location, appKpiService){
    if ($scope.trendStat.mrStats.length === 0) $location.path($scope.rootPath + "trend");

    $scope.showCharts = function () {
        $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
        $("#precise").highcharts(appKpiService.getPreciseRateOptions($scope.trendStat.districtStats,
            $scope.trendStat.townStats));
    };
    $scope.timeMrConfig = appKpiService.getMrsDistrictOptions($scope.trendStat.mrStats,
        $scope.trendStat.districts);
    $scope.timePreciseConfig = appKpiService.getPreciseDistrictOptions($scope.trendStat.preciseStats,
        $scope.trendStat.districts);
});
