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
    $scope.pciNeighbors = [];
    $scope.interferencePanelTitle = "干扰小区列表";

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
        }).success(function (result) {
            $scope.topCells = result;
        });
    };
    $scope.showInterference = function(cell) {
        $scope.currentCell = cell;
        $scope.interferenceCells = [];
        $scope.interferencePanelTitle = cell.eNodebName + "-" + cell.sectorId + "干扰小区列表";
        
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
    };
    $scope.showInfo = function(cell) {
        $scope.showInterference(cell);
    };
    $scope.match = function(cell) {
        $http({
            method: 'GET',
            url: $scope.dataModel.cellUrl,
            params: {
                'eNodebId': $scope.currentCell.cellId,
                'sectorId': $scope.currentCell.sectorId,
                'pci': cell.destPci
            }
        }).success(function(result) {
            $scope.pciNeighbors = result;
            $scope.dialogTitle = $scope.currentCell.eNodebName + "-" + $scope.currentCell.sectorId + "的邻区PCI=" + cell.destPci + "的可能小区";
            $(".modal").modal("show");
        });
    };

    $scope.query();
});