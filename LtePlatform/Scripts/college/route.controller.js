app.config(function($stateProvider, $urlRouterProvider) {
        var viewDir = "/appViews/College/";
        $stateProvider
            .state('map', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "AllMap.html",
                        controller: "all.map"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenuType.html",
                        controller: "college.menu"
                    }
                },
                url: "/"
            }).state('collegeMap', {
                abstract: true,
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "AllMap.html",
                        controller: "map.name"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenuType.html",
                        controller: "college.menu"
                    }
                },
                url: "/map/:name"
            }).state('collegeMap.lte', {
                url: "/lte"
            }).state('query', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "/Table/CollegeStatTable.html",
                        controller: "all.query"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenu.html",
                        controller: "college.menu"
                    }
                },
                url: "/query"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function($rootScope, collegeService) {
        var rootUrl = "/College/Map#";
        $rootScope.menuItems = [];
        $rootScope.rootPath = rootUrl + "/";

        $rootScope.collegeInfo = {
            year: {
                options: [2015, 2016],
                selected: 2015
            },
            url: $rootScope.rootPath + "map",
            names: [],
            type: ""
        };
        $rootScope.page = {
            title: "校园网总览"
        };
        collegeService.queryNames().then(function(result) {
            $rootScope.collegeInfo.names = result;
        });
    });
