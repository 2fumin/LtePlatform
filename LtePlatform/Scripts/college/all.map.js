app.controller("all.map", function ($scope, $uibModal, $log,  baiduMapService, collegeMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";

    var showCollegDialogs = function(college) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/appViews/College/Table/CollegeMapInfoBox.html',
            controller: 'map.college.dialog',
            size: 'sm',
            resolve: {
                dialogTitle: function () {
                    return college.name + "-" + "基本信息";
                },
                college: function () {
                    return college;
                }
            }
        });
        modalInstance.result.then(function (info) {
            console.log(info);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    baiduMapService.initializeMap("all-map", 11);
    baiduMapService.addCityBoundary("佛山");

    collegeMapService.showCollegeInfos(showCollegDialogs);
});