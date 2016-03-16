app.controller("menu.town", function ($scope, $stateParams) {
    $scope.menuTitle = $stateParams.city + $stateParams.district + $stateParams.town + "基础信息";
    var rootUrl = "/Parameters/List#";
    $scope.menuItems = [
        {
            displayName: "基础数据总揽",
            url: rootUrl + "/"
        }, {
            displayName: "小区地图查询",
            url: rootUrl + "/query"
        }, {
            displayName: $stateParams.city + $stateParams.district + $stateParams.town + "LTE基站列表",
            url: rootUrl + "/eNodebList" + "/" + $stateParams.city + "/" + $stateParams.district + "/" + $stateParams.town
        }, {
            displayName: $stateParams.city + $stateParams.district + $stateParams.town + "CDMA基站列表",
            url: rootUrl + "/btsList" + "/" + $stateParams.city + "/" + $stateParams.district + "/" + $stateParams.town
        }
    ];
});