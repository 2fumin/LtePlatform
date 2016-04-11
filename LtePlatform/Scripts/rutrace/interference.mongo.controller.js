
app.controller('interference.mongo', function ($scope, $uibModal, $log, dumpProgress, networkElementService, dumpPreciseService) {
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
                cell.dumpInfo = "未开始";
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

    $scope.generateDumpRecords = function (dumpRecords, startDate, endDate, eNodebId, sectorId, pci) {
        if (startDate >= endDate) {
            dumpPreciseService.dumpAllRecords(dumpRecords, 0, 0, eNodebId, sectorId, $scope.dump);
            return;
        }
        var date = new Date(startDate);
        dumpProgress.queryExistedItems(eNodebId, sectorId, date).then(function (existed) {
            dumpProgress.queryMongoItems(eNodebId, pci, existed.date).then(function (records) {
                dumpRecords.push({
                    date: records.date,
                    existedRecords: existed.value,
                    mongoRecords: records.value
                });
                startDate.setDate(date.getDate() + 1);
                $scope.generateDumpRecords(dumpRecords, startDate, endDate, eNodebId, sectorId, pci);
            });
        });
    };

    $scope.dump = function () {
        if ($scope.progressInfo.totalSuccessItems >= $scope.progressInfo.dumpCells.length) return;
        var cell = $scope.progressInfo.dumpCells[$scope.progressInfo.totalSuccessItems];
        cell.dumpInfo = "已导入";
        $scope.progressInfo.totalSuccessItems += 1;
        var eNodebId = cell.eNodebId;
        var sectorId = cell.sectorId;
        var pci = cell.pci;
        var begin = $scope.beginDate.value;
        var startDate = new Date(begin);
        var end = $scope.endDate.value;
        var endDate = new Date(end);
        var dumpRecords = [];
        $scope.generateDumpRecords(dumpRecords, startDate, endDate, eNodebId, sectorId, pci);
    };

    $scope.reset();
});