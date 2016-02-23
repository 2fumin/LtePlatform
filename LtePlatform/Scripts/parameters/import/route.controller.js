app.config([
    '$routeProvider', function($routeProvider) {
        var viewDir = "/appViews/Parameters/Import/";
        $routeProvider
            .when('/', {
                templateUrl: viewDir + "Index.html",
                controller: "import.index"
            })
            .when('/trend', {
                templateUrl: viewDir + "Trend.html",
                controller: "rutrace.trend"
            })
            .when('/top', {
                templateUrl: viewDir + "Top.html",
                controller: "rutrace.top"
            })
            .when('/chart', {
                templateUrl: viewDir + "Chart.html",
                controller: "rutrace.chart"
            })
            .when('/trendchart', {
                templateUrl: viewDir + "TrendChart.html",
                controller: "rutrace.trendchart"
            })
            .when('/top', {
                templateUrl: viewDir + "Top.html",
                controller: "rutrace.top"
            })
            .when('/import', {
                templateUrl: viewDir + "Import.html",
                controller: "rutrace.import"
            })
            .when('/interference', {
                templateUrl: viewDir + "Interference/Index.html",
                controller: "rutrace.interference"
            })
            .when('/baidumap', {
                templateUrl: viewDir + "Map/Index.html",
                controller: "rutrace.map"
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]).run(function($rootScope, basicImportService) {
    $rootScope.rootPath = "/Parameters/BasicImport#/";
    $rootScope.importData = {
        newENodebs: [],
        newCells: [],
        newBtss: [],
        newCdmaCells: [],
        vanishedENodebIds: [],
        vanishedCellIds: [],
        updateMessages: []
    };

    $rootScope.closeAlert = function (index) {
        $rootScope.importData.updateMessages.splice(index, 1);
    };

    basicImportService.queryENodebExcels().then(function(data) {
        $rootScope.newENodebs = data;
    });
    basicImportService.queryCellExcels().then(function (data) {
        $rootScope.newCells = data;
    });
    basicImportService.queryBtsExcels().then(function (data) {
        $rootScope.newBtss = data;
    });
    basicImportService.queryCdmaCellExcels().then(function (data) {
        $rootScope.newCdmaCells = data;
    });
    basicImportService.queryVanishedENodebs().then(function (data) {
        $rootScope.vanishedENodebIds = data;
    });
    basicImportService.queryVanishedCells().then(function (data) {
        $rootScope.vanishedCellIds = data;
    });
});