app.config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Test/Help/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "ApiGroup.html",
                    controller: "api.group"
                })
                .when('/method/:name', {
                    templateUrl: viewDir + "ApiMethod.html",
                    controller: "api.method"
                })
                .when('/api/:apiId/:method', {
                    templateUrl: viewDir + "Api.html",
                    controller: "api.details"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Help#";
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "",
            introduction: ""
        };
    });