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
            .when('/vanishedENodebInfos', {
                templateUrl: viewDir + "VanishedENodebs.html",
                controller: "eNodeb.vanished"
            })
            .when('/vanishedCellInfos', {
                templateUrl: viewDir + "VanishedCellInfos.html",
                controller: "cell.vanished"
            })
            .when('/btsInfos', {
                templateUrl: viewDir + "BtsInfos.html",
                controller: "import.btss"
            })
            .when('/btsLonLat', {
                templateUrl: viewDir + "BtsLonLatTable.html",
                controller: "bts.lonLat"
            })
            .when('/cdmaCellInfos', {
                templateUrl: viewDir + "CdmaCellInfos.html",
                controller: "import.cdmaCells"
            })
            .when('/cdmaCellLonLat', {
                templateUrl: viewDir + "CdmaCellLonLatTable.html",
                controller: "cdmaCell.lonLat"
            })
            .when('/vanishedBtsInfos', {
                templateUrl: viewDir + "VanishedBtss.html",
                controller: "bts.vanished"
            })
            .when('/vanishedCdmaCellInfos', {
                templateUrl: viewDir + "VanishedCdmaCellInfos.html",
                controller: "cdmaCell.vanished"
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
        vanishedBtsIds: [],
        vanishedCdmaCellIds: [],
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