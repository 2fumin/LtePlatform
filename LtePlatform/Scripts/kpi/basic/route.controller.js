app.config([
        '$routeProvider', function ($routeProvider) {
            var rootDir = "/appViews/BasicKpi/";
            $routeProvider
                .when('/', {
                    templateUrl: rootDir + 'Index.html',
                    controller: "kpi.basic"
                })
                .when('/trend/:city', {
                    templateUrl: rootDir + 'Trend.html',
                    controller: 'kpi.trend'
                })
                .when('/chart', {
                    templateUrl: rootDir + 'Charts.html',
                    controller: 'kpi.workitem.chart'
                })
                .when('/eNodeb/:eNodebId/:serialNumber', {
                    templateUrl: rootDir + 'ENodebInfo.html',
                    controller: 'workitem.eNodeb'
                })
                .when('/bts/:btsId/:serialNumber', {
                    templateUrl: rootDir + 'BtsInfo.html',
                    controller: "workitem.bts"
                })
                .when('/cell/:eNodebId/:sectorId/:serialNumber', {
                    templateUrl: rootDir + 'CellInfo.html',
                    controller: "workitem.cell"
                })
                .when('/cdmaCell/:btsId/:sectorId/:serialNumber', {
                    templateUrl: rootDir + 'CdmaCellInfo.html',
                    controller: "workitem.cdmaCell"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
])
    .run(function ($rootScope, appRegionService) {
        var rootUrl = "/Kpi#";
        $rootScope.menuItems = [
            {
                displayName: "指标总体情况",
                url: rootUrl + "/"
            }
        ];
        $rootScope.city = {
            selected: "",
            options: []
        };
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "指标总体情况",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };

        appRegionService.initializeCities()
            .then(function (result) {
                $rootScope.city = result;
                for (var i = 0; i < result[i]; i++) {
                    $rootScope.menuItems.push({
                        displayName: "指标变化趋势-" + result[i],
                        url: rootUrl + "/trend/" + result[i]
                    });
                }
            });
    });