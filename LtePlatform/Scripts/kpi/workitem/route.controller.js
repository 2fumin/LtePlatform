app.config([
        '$routeProvider', function ($routeProvider) {
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
                .when('/roleUser/:name', {
                    templateUrl: '/appViews/Manage/RoleUser.html',
                    controller: 'manage.roleUser'
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
    .run(function ($rootScope) {
        var rootUrl = "/Kpi/WorkItem#";
        $rootScope.menuItems = [
            {
                displayName: "工单总览",
                url: rootUrl + "/"
            }, {
                displayName: "所有用户信息管理",
                url: rootUrl + "/all"
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
        $rootScope.viewData = {
            items: []
        };
    });