app.controller("precise.import", function ($scope, preciseImportService) {
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.progressInfo = {
        totalDumpItems: 0,
        totalSuccessItems: 0,
        totalFailItems: 0
    };
    $scope.dumpHistory = [];
    $scope.townPreciseViews = [];

    $scope.clearItems = function() {
        preciseImportService.clearImportItems().then(function() {
            $scope.progressInfo.totalDumpItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
            $scope.progressInfo.totalFailItems = 0;
            $scope.townPreciseViews = [];
        });
    };
    $scope.dumpTownItems = function() {
        preciseImportService.dumpTownItems($scope.townPreciseViews).then(function() {
            $scope.townPreciseViews = [];
            $scope.updateHistory();
        });
    };
    $scope.updateHistory = function() {
        preciseImportService.queryDumpHistroy($scope.beginDate.value, $scope.endDate.value).then(function(result) {
            $scope.dumpHistory = result;
        });
    };
    $scope.dumpItems = function() {
        preciseImportService.dumpSingleItem().then(function(result) {
            if (result) {
                $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
            } else {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            }
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.updateHistory();
                if ($scope.townPreciseViews.length > 0) {
                    $scope.dumpTownItems();
                }
                
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        }, function() {
            $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
            if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                $scope.dumpItems();
            } else {
                $scope.updateHistory();
                if ($scope.townPreciseViews.length > 0) {
                    $scope.dumpTownItems();
                }

                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            }
        });
    };

    $scope.updateHistory();
   
    preciseImportService.queryTotalDumpItems().then(function (result) {
        $scope.progressInfo.totalDumpItems = result;
    });

    preciseImportService.queryTownPreciseViews().then(function (result) {
        $scope.townPreciseViews = result;
    });
});