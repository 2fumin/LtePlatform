app.controller("menu.town", function ($scope, $stateParams, menuItemService) {
    $scope.menuTitle = $stateParams.city + $stateParams.district + $stateParams.town + "基础信息";
    
    menuItemService.updateMenuItem($scope.menuItems, 0,
        $stateParams.city + $stateParams.district + $stateParams.town + "LTE基站列表",
        $scope.rootPath + "eNodebList" + "/" + $stateParams.city + "/" + $stateParams.district + "/" + $stateParams.town);
    menuItemService.updateMenuItem($scope.menuItems, 0,
        $stateParams.city + $stateParams.district + $stateParams.town + "CDMA基站列表",
        $scope.rootPath + "btsList" + "/" + $stateParams.city + "/" + $stateParams.district + "/" + $stateParams.town);
});