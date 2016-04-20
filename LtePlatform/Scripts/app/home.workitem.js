app.controller("home.workitem", function ($scope, workitemService) {
    workitemService.queryCurrentMonth().then(function (result) {
        $scope.totalItems = result.item1;
        $scope.finishedItems = result.item2;
        $scope.lateItems = result.item3;
        var finishedGauge = new GaugeMeter();
        var inTimeGauge = new GaugeMeter();
        finishedGauge.title.text = '完成工单情况';
        finishedGauge.yAxis.max = $scope.totalItems;
        finishedGauge.yAxis.plotBands[0].to = $scope.totalItems * 0.6;
        finishedGauge.yAxis.plotBands[1].from = $scope.totalItems * 0.6;
        finishedGauge.yAxis.plotBands[1].to = $scope.totalItems * 0.8;
        finishedGauge.yAxis.plotBands[2].from = $scope.totalItems * 0.8;
        finishedGauge.yAxis.plotBands[2].to = $scope.totalItems;
        finishedGauge.series[0].name = '完成工单数';
        finishedGauge.series[0].data[0] = $scope.finishedItems;
        inTimeGauge.title.text = '工单及时性';
        inTimeGauge.yAxis.max = $scope.totalItems;
        inTimeGauge.yAxis.plotBands[0].to = $scope.totalItems * 0.6;
        inTimeGauge.yAxis.plotBands[1].from = $scope.totalItems * 0.6;
        inTimeGauge.yAxis.plotBands[1].to = $scope.totalItems * 0.8;
        inTimeGauge.yAxis.plotBands[2].from = $scope.totalItems * 0.8;
        inTimeGauge.yAxis.plotBands[2].to = $scope.totalItems;
        inTimeGauge.series[0].name = '未超时工单数';
        inTimeGauge.series[0].data[0] = $scope.totalItems - $scope.lateItems;
        $("#workitemFinished").highcharts(finishedGauge.options);
        $("#workitemInTime").highcharts(inTimeGauge.options);
    });
});