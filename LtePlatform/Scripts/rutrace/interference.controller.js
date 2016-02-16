app.controller("rutrace.interference", function ($scope, $http) {
    $scope.pageTitle = "干扰矩阵分析";
    $scope.beginDate = {
        title: "开始日期",
        value: (new Date()).getDateFromToday(-7).Format("yyyy-MM-dd")
    };
    $scope.endDate = {
        title: "结束日期",
        value: (new Date()).Format("yyyy-MM-dd")
    };
    $scope.showinfo = {
        title: "干扰分析"
    };
    $scope.dataModel = new AppDataModel();
    $scope.dataModel.initializeAuthorization();
    $scope.currentCell = {};
    $scope.interferenceCells = [];
    $scope.interferenceLevelOrder = "interferenceLevel";
    $scope.interferencePanelTitle = "干扰小区列表";
    $scope.updateNeighborCounts = 0;

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

    $scope.showInterference = function(cell) {
        $scope.currentCell = cell;
        $scope.interferenceCells = [];
        $scope.updateNeighborCounts = 0;
        $scope.interferencePanelTitle = cell.eNodebName + "-" + cell.sectorId + "干扰小区列表";

        $http({
            method: 'GET',
            url: $scope.dataModel.cellUrl,
            params: {
                'eNodebId': cell.cellId,
                'sectorId': cell.sectorId
            }
        }).success(function(result) {
            $scope.currentCell.longtitute = result.longtitute;
            $scope.currentCell.lattitute = result.lattitute;
        });
        
        $http({
            method: 'GET',
            url: $scope.dataModel.interferenceNeighborUrl,
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
            }
        }).success(function () {
            $http({
                method: 'GET',
                url: $scope.dataModel.interferenceNeighborUrl,
                params: {
                    'begin': $scope.beginDate.value,
                    'end': $scope.endDate.value,
                    'cellId': cell.cellId,
                    'sectorId': cell.sectorId
                },
                headers: {
                    'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
                }
            }).success(function (result) {
                $scope.interferenceCells = result;
            });
        });

        $http({
            method: 'GET',
            url: $scope.dataModel.interferenceNeighborUrl,
            params: {
                neighborCellId: cell.cellId,
                neighborSectorId: cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
            }
        }).success(function(result) {
            $scope.updateNeighborCounts = result;
        });
    };
    $scope.showInfo = function(cell) {
        $scope.showInterference(cell);
    };
});