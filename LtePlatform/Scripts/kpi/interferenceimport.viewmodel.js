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
    $scope.panelTitle = "从数据文件导入";

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

app.controller('interference.mongo', function ($scope, $http, dumpProgress) {
    $scope.dataModel = new AppDataModel();
    $scope.progressInfo = {
        dumpCells: [],
        totalSuccessItems: 0,
        totalFailItems: 0,
        cellInfo: ""
    };
    $scope.panelTitle = "从MongoDB导入";
    $scope.beginDate = {
        title: "开始日期",
        value: (new Date()).getDateFromToday(-7).Format("yyyy-MM-dd")
    };
    $scope.endDate = {
        title: "结束日期",
        value: (new Date()).Format("yyyy-MM-dd")
    };

    $scope.reset = function() {
        $http({
            method: 'GET',
            url: $scope.dataModel.dumpInterferenceUrl,
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value
            }
        }).success(function(result) {
            $scope.progressInfo.dumpCells = result;
            $scope.progressInfo.totalFailItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
        });
    };

    $scope.dump = function() {
        dumpProgress.dumpMongo($scope.dataModel.dumpInterferenceUrl, $scope.progressInfo, $scope.beginDate.value, $scope.endDate.value, 0);
    };

    $scope.reset();
});