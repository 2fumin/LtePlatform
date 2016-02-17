app.controller("rutrace.interference", function ($scope, $http, appUrlService) {
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
    $scope.currentCell = {};
    $scope.interferenceCells = [];
    $scope.victimCells = [];
    $scope.interferenceLevelOrder = "interferenceLevel";
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
        $scope.victimCells = [];
        $scope.updateNeighborCounts = 0;
        $scope.interferencePanelTitle = cell.eNodebName + "-" + cell.sectorId + "干扰小区列表";

        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('Cell'),
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
            url: appUrlService.getApiUrl('InterferenceNeighbor'),
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function () {
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('InterferenceNeighbor'),
                params: {
                    'begin': $scope.beginDate.value,
                    'end': $scope.endDate.value,
                    'cellId': cell.cellId,
                    'sectorId': cell.sectorId
                },
                headers: {
                    'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                }
            }).success(function (result) {
                $scope.interferenceCells = result;
            });
        });

        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('InterferenceVictim'),
            params: {
                'begin': $scope.beginDate.value,
                'end': $scope.endDate.value,
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function(result) {
            $scope.victimCells = result;
        });

        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('InterferenceNeighbor'),
            params: {
                neighborCellId: cell.cellId,
                neighborSectorId: cell.sectorId
            },
            headers: {
                'Authorization': 'Bearer ' + appUrlService.getAccessToken()
            }
        }).success(function(message) {
            $scope.updateNeighborCounts = message;
        });
    };
    $scope.showInfo = function(cell) {
        $scope.showInterference(cell);
    };
    $scope.refreshTopic = function (topic) {
        $scope.currentTopic = topic;
    };
});