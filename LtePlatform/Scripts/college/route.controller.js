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
                        templateUrl: viewDir + "CollegeMenu.html",
                        controller: "college.menu"
                    }
                },
                url: "/"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function($rootScope) {
        var rootUrl = "/College/Map#";
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
            title: "校园网总览"
        };
    });
