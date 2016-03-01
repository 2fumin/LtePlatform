app.config([
        '$routeProvider', function($routeProvider) {
            var rootDir = "/appViews/WorkItem/";
            $routeProvider
                .when('/', {
                    templateUrl: rootDir + 'List.html',
                    controller: "kpi.workitem"
                })
                .when('/details/:number', {
                    templateUrl: rootDir + 'Details.html',
                    controller: 'kpi.workitem.details'
                })
                .when('/chart', {
                    templateUrl: rootDir + 'Charts.html',
                    controller: 'kpi.workitem.chart'
                })
                .when('/eNodeb/:eNodebId', {
                    templateUrl: rootDir + 'ENodebInfo.html',
                    controller: 'workitem.eNodeb'
                })
                .when('/addPhoneNumber', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.signup"
                })
                .when('/modifyPhoneNumber/:number', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.modify"
                })
                .when('/changePassword', {
                    templateUrl: '/appViews/Manage/ChangePassword.html',
                    controller: "password.change"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Kpi/WorkItem#";
        $rootScope.menuItems = [
            {
                displayName: "工单总览",
                url: rootUrl + "/"
            }, {
                displayName: "统计图表",
                url: rootUrl + "/chart"
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
        $rootScope.closeAlert = function(index) {
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