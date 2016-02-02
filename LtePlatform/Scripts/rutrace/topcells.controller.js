app.controller("rutrace.topcells", function ($scope, $http) {
    $scope.topCells = [];
    $scope.cellPanelTitle = "TOP指标查询";

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

    $scope.query();
});