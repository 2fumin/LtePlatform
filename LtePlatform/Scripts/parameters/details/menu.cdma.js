app.controller("menu.cdma", function ($scope, $stateParams, networkElementService) {
    $scope.menuTitle = $stateParams.name + "详细信息";
    var rootUrl = "/Parameters/List#";
    $scope.menuItems = [
        {
            displayName: "基础数据总揽",
            url: rootUrl + "/"
        }, {
            displayName: "小区地图查询",
            url: rootUrl + "/query"
        }, {
            displayName: $stateParams.name + "基础信息",
            url: rootUrl + "/btsInfo" + "/" + $stateParams.btsId + "/" + $stateParams.name
        }
    ];
    networkElementService.queryCdmaSectorIds($stateParams.name).then(function (result) {
        angular.forEach(result, function(sectorId) {
            $scope.menuItems[1].push({
                displayName: $stateParams.name + "-" + sectorId + "小区信息",
                url: rootUrl + "/cdmaCellInfo" + "/" + $stateParams.btsId + "/" + $stateParams.name + "/" + sectorId
            });
        });
    });
});