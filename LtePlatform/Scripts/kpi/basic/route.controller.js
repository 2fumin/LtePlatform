app.config([
        '$routeProvider', function ($routeProvider) {
            var rootDir = "/appViews/BasicKpi/";
            $routeProvider
                .when('/', {
                    templateUrl: rootDir + 'Index.html',
                    controller: "kpi.basic"
                })
                .when('/trend', {
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
    .run(function ($rootScope) {
        var rootUrl = "/Kpi#";
        $rootScope.menuItems = [
            {
                displayName: "指标总览",
                url: rootUrl + "/"
            }, {
                displayName: "变化趋势",
                url: rootUrl + "/trend"
            }, {
                displayName: "所有角色管理",
                url: rootUrl + "/roles"
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "工单总览",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };
        $rootScope.states = [
            {
                name: '未完成'
            }, {
                name: '全部'
            }
        ];
        $rootScope.types = [
            {
                name: '全部'
            }, {
                name: '2/3G'
            }, {
                name: '4G'
            }
        ];
        $rootScope.pageSizeSelection = [
            {
                value: 10
            }, {
                value: 15
            }, {
                value: 20
            }, {
                value: 30
            }, {
                value: 50
            }
        ];
        $rootScope.viewData = {
            items: [],
            currentState: $rootScope.states[0],
            currentType: $rootScope.types[0],
            itemsPerPage: $rootScope.pageSizeSelection[1],
            totalItems: 0,
            currentPage: 1
        };
    });