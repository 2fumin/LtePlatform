app.controller("workitem.details", function ($scope, $routeParams, workitemService, workItemDialog, appFormatService, cellPreciseService) {
    $scope.page.title = "工单编号" + $routeParams.number + "信息";
    var lastWeek = new Date();
    lastWeek.setDate(lastWeek.getDate() - 7);
    $scope.beginDate = {
        value: lastWeek,
        opened: false
    };
    $scope.endDate = {
        value: new Date(),
        opened: false
    };
    $scope.queryWorkItems = function () {
        workitemService.querySingleItem($routeParams.number).then(function (result) {
            $scope.currentView = result;
            $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
            $scope.beginDate.value = appFormatService.getDate($scope.currentView.beginTime);
            $scope.showTrend();
        });
    };
    $scope.feedback = function (view) {
        workItemDialog.feedback(view, $scope.queryWorkItems);
    };
    $scope.showTrend = function () {
        $scope.beginDateString = appFormatService.getDateString($scope.beginDate.value, "yyyy年MM月dd日");
        $scope.endDateString = appFormatService.getDateString($scope.endDate.value, "yyyy年MM月dd日");
        cellPreciseService.queryDataSpanKpi($scope.beginDate.value, $scope.endDate.value, $scope.currentView.eNodebId,
            $scope.currentView.sectorId).then(function (result) {
                $scope.mrsConfig = cellPreciseService.getMrsOptions(result,
                    $scope.beginDateString + "-" + $scope.endDateString + "MR数变化趋势");
                $scope.preciseConfig = cellPreciseService.getPreciseOptions(result,
                    $scope.beginDateString + "-" + $scope.endDateString + "精确覆盖率变化趋势");
            });
    };
    $scope.analyzeInterferenceSource = function() {
        
    };
    $scope.queryWorkItems();
});