app.controller("menu.root", function($scope) {
    $scope.menuTitle = "功能列表";
    var rootUrl = "/Parameters/List#";
    $scope.menuItems = [
        {
            displayName: "基础数据总揽",
            url: rootUrl + "/"
        }, {
            displayName: "小区地图查询",
            url: rootUrl + "/query"
        }, {
            displayName: "TOP指标分析",
            url: rootUrl + "/top"
        }
    ];
});