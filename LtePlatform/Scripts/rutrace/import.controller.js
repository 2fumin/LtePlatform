app.controller("rutrace.import", function ($scope, $http, appUrlService) {
    $scope.pageTitle = "TOP小区导入设置";
    $scope.beginDate = {
        title: "开始日期",
        value: (new Date()).getDateFromToday(-7).Format("yyyy-MM-dd")
    };
    $scope.endDate = {
        title: "结束日期",
        value: (new Date()).Format("yyyy-MM-dd")
    };
    $scope.showinfo = {
        title: "邻区列表"
    };
    $scope.currentCell = {};
    $scope.neighborCells = [];
    $scope.updateMessages = [];

    $scope.showNeighbors = function(cell) {
        $scope.currentCell = cell;
        $scope.neighborCells = [];
        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('NearestPciCell'),
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            }
        }).success(function(result) {
            $scope.neighborCells = result;
        });
    };
    $scope.showInfo = function(cell) {
        $scope.showNeighbors(cell);
    };
    $scope.updatePci = function(cell) {
        $http.post(appUrlService.getApiUrl('NearestPciCell'), cell).success(function (result) {
            $scope.updateMessages.push({
                cellName: cell.eNodebName + '-' + cell.sectorId,
                counts: result
            });
            $scope.showNeighbors(cell);
        });
    };
    $scope.closeAlert = function (index) {
        $scope.updateMessages.splice(index, 1);
    }
    $scope.addMonitor = function(cell) {
        $http.post(appUrlService.getApiUrl('NeighborMonitor'), {
            cellId: cell.cellId,
            sectorId: cell.sectorId
        }).success(function() {
            cell.isMonitored = true;
        });
    };
    $scope.addNeighborMonitor = function (cell) {
        $http.post(appUrlService.getApiUrl('NeighborMonitor'), {
            cellId: cell.nearestCellId,
            sectorId: cell.nearestSectorId
        }).success(function () {
            cell.isMonitored = true;
        });
    };
    $scope.monitorNeighbors = function() {
        for (var i = 0; i < $scope.neighborCells.length; i++) {
            var cell = $scope.neighborCells[i];
            if (cell.isMonitored === false) {
                $scope.addNeighborMonitor(cell);
            }
        }
    };
});