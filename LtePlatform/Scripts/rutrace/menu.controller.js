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
                .when('/topDistrict/:district', {
                    templateUrl: viewDir + "Top.html",
                    controller: "rutrace.top.district"
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
                .when('/import/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + "Import.html",
                    controller: "rutrace.import"
                })
                .when('/interference/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + "Interference/Index.html",
                    controller: "rutrace.interference"
                })
                .when('/baidumap/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + "Map/Index.html",
                    controller: "rutrace.map"
                })
                .when('/details/:number', {
                    templateUrl: viewDir + "WorkItem/AnalyticDetails.html",
                    controller: "workitem.details"
                })
                .when('/workItems/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + "WorkItem/ForCell.html",
                    controller: "rutrace.workitems"
                })
                .when('/workitemDistrict/:district', {
                    templateUrl: viewDir + "WorkItem/ForCity.html",
                    controller: "workitem.district"
                })
                .when('/workitemCity', {
                    templateUrl: viewDir + "WorkItem/ForCity.html",
                    controller: "workitem.city"
                })
                .when('/cellTrend/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + "WorkItem/CellTrend.html",
                    controller: "cell.trend"
                })
                .when('/mongo', {
                    templateUrl: '/appViews/FromMongo.html',
                    controller: 'interference.mongo'
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
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "指标总体情况",
                        url: rootUrl + "/"
                    }, {
                        displayName: "指标变化趋势",
                        url: rootUrl + "/trend"
                    }
                ]
            }, {
                displayName: "详细查询",
                isActive: false,
                subItems: []
            }, {
                displayName: "TOP指标",
                isActive: true,
                subItems: [
                    {
                        displayName: "分析",
                        url: rootUrl + "/top"
                    }
                ]
            }, {
                displayName: "辅助功能",
                isActive: false,
                subItems: [
                    {
                        displayName: "从MongoDB导入",
                        url: rootUrl + "/mongo"
                    }
                ]
            }
        ];
        $rootScope.rootPath = rootUrl + "/";

        $rootScope.viewData = {
            workItems: []
        };
    });
