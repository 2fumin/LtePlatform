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
                .when('/topDrop2G', {
                    templateUrl: rootDir + 'TopDrop2G.html',
                    controller: 'kpi.topDrop2G'
                })
                .when('/topDrop2GTrend/:cellId/:sectorId', {
                    templateUrl: rootDir + 'TopDrop2GTrend.html',
                    controller: 'kpi.topDrop2G.trend'
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
            }, {
                displayName: "TOP掉话指标",
                url: rootUrl + "/topDrop2G"
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "指标总体情况",
            messages: []
        };
        $rootScope.topData = {
            drop2G: [],
            connection3G: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };

        appRegionService.initializeCities()
            .then(function (result) {
                for (var i = 0; i < result.options.length; i++) {
                    $rootScope.menuItems.push({
                        displayName: "指标变化趋势-" + result.options[i],
                        url: rootUrl + "/trend/" + result.options[i]
                    });
                }
            });
    });