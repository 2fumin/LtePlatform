app.controller("rutrace.import", function ($scope) {
    $scope.pageTitle = "TOP小区导入设置";
    $scope.beginDate = {
        title: "开始日期",
        value: "2016-01-04"
    };
    $scope.endDate = {
        title: "结束日期",
        value: "2016-01-14"
    };
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
});