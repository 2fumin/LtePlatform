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

app.controller("interference.root", function($scope) {
    $scope.dataModel = new AppDataModel();
    $scope.progressInfo = {};
    $scope.beginDate = {
        title: "开始日期",
        value: (new Date()).getDateFromToday(-7).Format("yyyy-MM-dd")
    };
    $scope.endDate = {
        title: "结束日期",
        value: (new Date()).Format("yyyy-MM-dd")
    };
    $('.form_date').datetimepicker({
        language: 'zh-CN',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0
    });
});
