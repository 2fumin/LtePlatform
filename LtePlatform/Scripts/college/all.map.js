app.controller("all.map", function ($scope, $uibModal, $log,  baiduMapService, collegeMapService) {
    $scope.collegeInfo.url = $scope.rootPath + "map";

    var showCollegDialogs = function(college) {

    };

    baiduMapService.initializeMap("all-map", 11);
    baiduMapService.addCityBoundary("佛山");

    collegeMapService.showCollegeInfos(showCollegDialogs);
});