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
    $scope.currentCell = {};
    $scope.interferenceCells = [];

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
        $http({
            method: 'GET',
            url: $scope.dataModel.interferenceNeighborUrl,
            params: {
                'cellId': cell.cellId,
                'sectorId': cell.sectorId
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
                }
            }).success(function (result) {
                $scope.interferenceCells = result;
            });
        });
    };
    $scope.showInfo = function(cell) {
        $scope.showInterference(cell);
    };

    $scope.query();
});