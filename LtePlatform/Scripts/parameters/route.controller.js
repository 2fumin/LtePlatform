app.config(function ($stateProvider, $urlRouterProvider) {
        var viewDir = "/appViews/Parameters/";
        $stateProvider
            .state('list', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "List.html",
                        controller: "parameters.list"
                    }
                },
                url: "/"
            })
            .state('query', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.root"
                    },
                    "contents": {
                        templateUrl: viewDir + "QueryMap.html",
                        controller: "query.map"
                    }
                },
                url: "/query"
            })
            .state('eNodebList', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.town"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/ENodebTable.html",
                        controller: "eNodeb.list"
                    }
                },
                url: "/eNodebList/:city/:district/:town"
            })
            .state('btsList', {
                views: {
                    'menu': {
                        templateUrl: "/appViews/GeneralMenu.html",
                        controller: "menu.town"
                    },
                    "contents": {
                        templateUrl: viewDir + "Region/BtsTable.html",
                        controller: "bts.list"
                    }
                },
                url: "/btsList/:city/:district/:town"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function($rootScope) {
        var rootUrl = "/Parameters/List#";
        $rootScope.menuItems = [];
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
        $rootScope.page = {
            title: "基础数据总揽"
        };
    });
