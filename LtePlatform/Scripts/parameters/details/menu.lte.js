app.controller("menu.lte", function ($scope, $stateParams, networkElementService, menuItemService) {
    $scope.menuTitle = $stateParams.name + "详细信息";
    
    menuItemService.updateMenuItem($scope.menuItems, 1,
        $stateParams.name + "LTE基础信息",
        $scope.rootPath + "eNodebInfo" + "/" + $stateParams.eNodebId + "/" + $stateParams.name);
    $scope.menu.accordions[$stateParams.name + "LTE基础信息"] = true;
    
    networkElementService.queryCellSectorIds($stateParams.name).then(function (result) {
        angular.forEach(result, function (sectorId) {
            menuItemService.updateMenuItem($scope.menuItems, 1,
                $stateParams.name + "-" + sectorId + "小区信息",
                $scope.rootPath + "cellInfo" + "/" + $stateParams.eNodebId + "/" + $stateParams.name + "/" + sectorId,
                $stateParams.name + "LTE基础信息");
        });
    });
});