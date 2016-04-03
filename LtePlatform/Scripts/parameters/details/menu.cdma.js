app.controller("menu.cdma", function ($scope, $stateParams, networkElementService, menuItemService) {
    $scope.menuTitle = $stateParams.name + "详细信息";

    menuItemService.updateMenuItem($scope.menuItems, 1,
        $stateParams.name + "基础信息",
        $scope.rootPath + "btsInfo" + "/" + $stateParams.btsId + "/" + $stateParams.name);
    networkElementService.queryCdmaSectorIds($stateParams.name).then(function (result) {
        angular.forEach(result, function (sectorId) {
            menuItemService.updateMenuItem($scope.menuItems, 1,
                $stateParams.name + "-" + sectorId + "小区信息",
                $scope.rootPath + "cdmaCellInfo" + "/" + $stateParams.btsId + "/" + $stateParams.name + "/" + sectorId);
        });
    });
});