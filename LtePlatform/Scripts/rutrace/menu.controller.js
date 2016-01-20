app.controller("rutrace.menu", function ($scope) {
    $scope.menuItems = [
    {
        name: "TOP小区导入设置",
        displayName: "导入设置",
        url: "/Rutrace/Import"
    }, {
        displayName: "小区分析",
        items: [
        {
            url: "/Rutrace/Interference",
            displayName: "干扰分析"
        }, {
            url: "/Rutrace/Coverage",
            displayName: "覆盖分析"
        }]
    }, {
        name: "精确覆盖优化工单一览",
        displayName: "工单管理",
        url: "/Rutrace/WorkItem"
    }];
});