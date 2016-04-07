
app.controller('interference.mongo', function ($scope, $uibModal, $log, dumpProgress, networkElementService) {
    $scope.progressInfo = {
        dumpCells: [],
        totalSuccessItems: 0,
        totalFailItems: 0,
        cellInfo: ""
    };
    $scope.page.title = "从MongoDB导入";
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: new Date(lastWeek.getFullYear(), lastWeek.getMonth(), lastWeek.getDate(), 8),
        opened: false
    };
    var today = new Date();
    $scope.endDate = {
        value: new Date(today.getFullYear(), today.getMonth(), today.getDate(), 8),
        opened: false
    };
    $scope.currentPage = 1;

    $scope.reset = function () {
        dumpProgress.resetProgress($scope.beginDate.value, $scope.endDate.value).then(function(result) {
            $scope.progressInfo.dumpCells = result;
            $scope.progressInfo.totalFailItems = 0;
            $scope.progressInfo.totalSuccessItems = 0;
            angular.forEach($scope.progressInfo.dumpCells, function(cell) {
                networkElementService.queryENodebInfo(cell.eNodebId).then(function(eNodeb) {
                    cell.name = eNodeb.name;
                });
            });
        });
    };

    $scope.dumpMongo = function(cell) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/Rutrace/Interference/DumpCellMongoDialog.html',
            controller: 'dump.cell.mongo',
            size: 'lg',
            resolve: {
                dialogTitle: function () {
                    return cell.name + "-" + cell.sectorId + "干扰数据导入";
                },
                eNodebId: function() {
                    return cell.eNodebId;
                },
                sectorId: function() {
                    return cell.sectorId;
                },
                pci: function() {
                    return cell.pci;
                },
                begin: function() {
                    return $scope.beginDate.value;
                },
                end: function() {
                    return $scope.endDate.value;
                }
            }
        });

        modalInstance.result.then(function (info) {
            console.log(info);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.dump = function () {
        
    };

    $scope.reset();
});