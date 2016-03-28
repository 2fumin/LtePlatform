app.controller("all.query", function($scope, $uibModal, $log, collegeService) {
    $scope.collegeInfo.url = $scope.rootPath + "query";
    $scope.page.title = "基础信息查看";

    $scope.showENodebs = function(name) {
        alert(name);
    };

    collegeService.queryStats().then(function(colleges) {
        $scope.collegeList = colleges;
    });
});