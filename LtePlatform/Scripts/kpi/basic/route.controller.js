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
                .when('/topConnection3G', {
                    templateUrl: rootDir + 'TopConnection3G.html',
                    controller: 'kpi.topConnection3G'
                })
                .when('/topDrop2GTrend/:city', {
                    templateUrl: rootDir + 'TopDrop2GTrend.html',
                    controller: 'kpi.topDrop2G.trend'
                })
                .when('/topConnection3GTrend/:city', {
                    templateUrl: rootDir + 'TopConnection3GTrend.html',
                    controller: 'kpi.topConnection3G.trend'
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
    .run(function ($rootScope, appRegionService, menuItemService) {
        var rootUrl = "/Kpi#";
        $rootScope.menuItems = [
            {
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "指标总体情况",
                        url: rootUrl + "/"
                    }
                ]
            }, {
                displayName: "TOP指标",
                isActive: true,
                subItems: [
                    {
                        displayName: "TOP掉话指标",
                        url: rootUrl + "/topDrop2G"
                    }, {
                        displayName: "TOP连接成功率指标",
                        url: rootUrl + "/topConnection3G"
                    }
                ]
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
                angular.forEach(result.options, function(district) {
                    menuItemService.updateMenuItem($rootScope.menuItems, 0,
                        "指标变化趋势-" + district,
                        $rootScope.rootPath + "trend/" + district);
                    menuItemService.updateMenuItem($rootScope.menuItems, 1,
                        "TOP掉话变化趋势-" + district,
                        $rootScope.rootPath + "topDrop2GTrend/" + district);
                    menuItemService.updateMenuItem($rootScope.menuItems, 1,
                        "TOP掉话变化趋势-" + district,
                        $rootScope.rootPath + "topConnection3GTrend/" + district);
                });
            });
    });