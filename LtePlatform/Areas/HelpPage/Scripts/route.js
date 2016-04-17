app.config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Test/Help/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "ApiGroup.html",
                    controller: "api.group"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Help#";
        $rootScope.rootPath = rootUrl + "/";
    });