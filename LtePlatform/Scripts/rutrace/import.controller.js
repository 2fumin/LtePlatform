app.controller("rutrace.import", function ($scope, $http) {
    $scope.pageTitle = "TOP小区导入设置";
    $scope.beginDate = {
        title: "开始日期",
        value: (new Date()).getDateFromToday(-7).Format("yyyy-MM-dd")
    };
    $scope.endDate = {
        title: "结束日期",
        value: (new Date()).Format("yyyy-MM-dd")
    };
    $scope.topCells = [];
    $scope.dataModel = new AppDataModel();
    $scope.currentCell = {};
    $scope.neighborCells = [];
    $scope.updateCounts = 0;

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

    $scope.query = function () {
        $scope.topCells = [];
        $http({
            method: 'GET',
            url: $scope.dataModel.preciseStatUrl,
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value,
                'topCount': 20,
                'orderSelection': "按照精确覆盖率升序"
            }
        }).success(function(result) {
            $scope.topCells = result;
        });
    };
    $scope.showNeighbors = function(cell) {
        $scope.currentCell = cell;
        $scope.neighborCells = [];
        $http({
            method: 'GET',
            url: $scope.dataModel.nearestPciCellUrl,
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            }
        }).success(function(result) {
            $scope.neighborCells = result;
        });
    };
    $scope.updatePci = function(cell) {
        $http.post($scope.dataModel.nearestPciCellUrl, cell).success(function(result) {
            $scope.updateCounts = result;
            $scope.showNeighbors(cell);
        });
    };
    $scope.addMonitor = function(cell) {
        $http.post($scope.dataModel.neighborMonitorUrl, cell).success(function(result) {
            cell.isMonitored = true;
        });
    };
    $scope.monitorAll = function() {
        for (var i = 0; i < $scope.topCells.length; i++) {
            var cell = $scope.topCells[i];
            if (cell.isMonitored === false) {
                $scope.addMonitor(cell);
            }
        }
    };

    $scope.query();
});