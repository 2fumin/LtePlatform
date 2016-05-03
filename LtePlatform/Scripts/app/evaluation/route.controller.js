app.config([
        '$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
            var rootDir = "/appViews/Evaluation/";
            $routeProvider
                .when('/', {
                    templateUrl: rootDir + 'Home.html',
                    controller: "evaluation.home"
                })
                .when('/details/:number', {
                    templateUrl: rootDir + 'Details.html',
                    controller: 'kpi.workitem.details'
                })
                .when('/details/:number/:district', {
                    templateUrl: rootDir + 'Details.html',
                    controller: 'kpi.workitem.details.district'
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
                .when('/stat/:district', {
                    templateUrl: rootDir + 'DistrictList.html',
                    controller: "workitem.district"
                })
                .otherwise({
                    redirectTo: '/'
                });
            $httpProvider.defaults.useXDomain = true;
            delete $httpProvider.defaults.headers.common['X-Requested-With'];
        }
])
    .run(function ($rootScope) {
        var rootUrl = "/Evaluation/RegionDef#";
        $rootScope.menuItems = [
            {
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "万栋楼宇",
                        url: rootUrl + "/"
                    }, {
                        displayName: "统计图表",
                        url: rootUrl + "/chart"
                    }
                ]
            }, {
                displayName: "分区指标",
                isActive: true,
                subItems: []
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "万栋楼宇",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };
    });