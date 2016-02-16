app.config([
    '$routeProvider', function ($routeProvider) {
        var rootDir = "/appViews/Rutrace/Interference/";
        $routeProvider
            .when('/', {
                templateUrl: rootDir + "Source.html",
                controller: "interference.source"
            })
            .when('/victim', {
                templateUrl: rootDir + "Victim.html",
                controller: "interference.victim"
            })
            .when('/roles', {
                templateUrl: '/appViews/Manage/Roles.html',
                controller: 'manage.roles'
            })
            .when('/roleUser/:name', {
                templateUrl: '/appViews/Manage/RoleUser.html',
                controller: 'manage.roleUser'
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);

app.run(function ($rootScope) {
    // 使用.run访问$rootScope
    $rootScope.pageTitle = "干扰矩阵分析";
    $rootScope.dataModel = new AppDataModel();
    $rootScope.dataModel.initializeAuthorization();
    $rootScope.interferencePanelTitle = "干扰小区列表";
    $rootScope.currentTopic = "Source";

    var rootUrl = "/Rutrace/Interference#";
    $rootScope.routeInfos = [
    {
        url: rootUrl + '/',
        topic: "Source",
        displayName: "干扰小区"
    }, {
        url: rootUrl + '/victim',
        topic: "Victim",
        displayName: "被干扰小区"
    }];
});

app.controller("interference.source", function($scope) {
});

app.controller("interference.victim", function ($scope) {
});