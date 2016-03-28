app.controller("all.query", function($scope, $uibModal, $log, collegeService) {
    $scope.collegeInfo.url = $scope.rootPath + "query";
    $scope.page.title = "基础信息查看";

    $scope.showENodebs = function(name) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/College/Infrastructure/ENodebDialog.html',
            controller: 'eNodeb.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return name + "-" + "基站信息";
                },
                name: function () {
                    return name;
                }
            }
        });
        modalInstance.result.then(function (info) {
            console.log(info);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    collegeService.queryStats().then(function(colleges) {
        $scope.collegeList = colleges;
    });
});