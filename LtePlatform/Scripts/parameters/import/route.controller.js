app.config([
    '$routeProvider', function($routeProvider) {
        var viewDir = "/appViews/Parameters/Import/";
        $routeProvider
            .when('/', {
                templateUrl: viewDir + "Index.html",
                controller: "import.index"
            })
            .when('/eNodebInfos', {
                templateUrl: viewDir + "ENodebInfos.html",
                controller: "import.eNodebs"
            })
            .when('/eNodebLonLat', {
                templateUrl: viewDir + "ENodebLonLatTable.html",
                controller: "eNodeb.lonLat"
            })
            .when('/cellInfos', {
                templateUrl: viewDir + "CellInfos.html",
                controller: "import.cells"
            })
            .when('/cellLonLat', {
                templateUrl: viewDir + "CellLonLatTable.html",
                controller: "cell.lonLat"
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
        $rootScope.importData.newENodebs = data;
    });
    basicImportService.queryCellExcels().then(function (data) {
        $rootScope.importData.newCells = data;
    });
    basicImportService.queryBtsExcels().then(function (data) {
        $rootScope.importData.newBtss = data;
    });
    basicImportService.queryCdmaCellExcels().then(function (data) {
        $rootScope.importData.newCdmaCells = data;
    });
    basicImportService.queryVanishedENodebs().then(function (data) {
        $rootScope.importData.vanishedENodebIds = data;
    });
    basicImportService.queryVanishedCells().then(function (data) {
        $rootScope.importData.vanishedCellIds = data;
    });
});