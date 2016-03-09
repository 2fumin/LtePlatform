app.config(function ($stateProvider, $urlRouterProvider) {
        var viewDir = "/appViews/Rutrace/";
        $stateProvider
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
            .when('/import', {
                templateUrl: viewDir + "Import.html",
                controller: "rutrace.import"
            })
            .when('/interference', {
                templateUrl: viewDir + "Interference/Index.html",
                controller: "rutrace.interference"
            })
            .when('/baidumap/:name', {
                templateUrl: viewDir + "Map/Index.html",
                controller: "rutrace.map"
            })
            .when('/details/:number', {
                templateUrl: viewDir + "WorkItem/ForCell.html",
                controller: "workitem.details"
            })
            .when('/workItems/:cellId/:sectorId', {
                templateUrl: viewDir + "WorkItem/ForCell.html",
                controller: "rutrace.workitems"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function($rootScope) {
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

        $rootScope.updateMenuItems = function(namePrefix, urlPrefix, name) {
            var items = $rootScope.menuItems;
            for (var i = 0; i < items.length; i++) {
                if (items[i].displayName === namePrefix + "-" + name) return;
            }
            items.push({
                displayName: namePrefix + "-" + name,
                url: urlPrefix + "/" + name
            });
        };
        $rootScope.viewData = {
            workItems: []
        };
    });
