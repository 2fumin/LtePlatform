app.config([
    '$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/appViews/FromTxt.html',
                controller: "interference.import"
            })
            .when('/mongo', {
                templateUrl: '/appViews/FromMongo.html',
                controller: 'interference.mongo'
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);

app.controller("interference.import", function ($scope, $http, dumpProgress) {
    $scope.dataModel = new AppDataModel();
    $scope.progressInfo = {
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };

    $scope.clearItems = function () {
        dumpProgress.clear($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo);
    };

    $scope.dumpItems = function() {
        dumpProgress.dump($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo);
    };

    $http.get($scope.dataModel.dumpInterferenceUrl).success(function(result) {
        $scope.progressInfo.totalDumpItems = result;
    });
});

app.controller('interference.mongo', function($scope, $http) {

});