app.controller("rutrace.import", function ($scope) {
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
    $scope.query = function() {
        $scope.topCells = [];
        sendRequest($scope.dataModel.preciseStatUrl, "GET", {
            begin: $scope.beginDate.value,
            end: $scope.endDate.value,
            topCount: 20,
            orderSelection: "按照精确覆盖率升序"
        }, function (result) {
            $scope.topCells = result;
        });
    };
});