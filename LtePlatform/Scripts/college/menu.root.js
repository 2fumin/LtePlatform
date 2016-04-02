app.controller("menu.root", function($scope) {
    $scope.menuTitle = "功能列表";
    var rootUrl = "/College/Map#";
    $scope.menuItems = [
        {
            displayName: "覆盖情况",
            tag: "coverage",
            isActive: true,
            subItems: [
                {
                    displayName: "校园网总览",
                    url: rootUrl + "/"
                }, {
                    displayName: "基础信息查看",
                    url: rootUrl + "/query"
                }, {
                    displayName: "校园覆盖",
                    url: rootUrl + "/coverage"
                }
            ]
        }, {
            displayName: "日常管理",
            tag: "management",
            isActive: false,
            subItems: [
                {
                    displayName: "测试报告",
                    url: rootUrl + "/test"
                }, {
                    displayName: "指标报告",
                    url: rootUrl + "/kpi"
                }, {
                    displayName: "精确覆盖",
                    url: rootUrl + "/precise"
                }
            ]
        }
    ];
});