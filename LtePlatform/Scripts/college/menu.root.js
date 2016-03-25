﻿app.controller("menu.root", function($scope) {
    $scope.menuTitle = "功能列表";
    var rootUrl = "/College/Map#";
    $scope.menuItems = [
        {
        	displayName: "校园网总览",
            url: rootUrl + "/"
        }, {
        	displayName: "基础信息查看",
            url: rootUrl + "/query"
        }, {
        	displayName: "校园覆盖",
            url: rootUrl + "/coverage"
        }, {
        	displayName: "测试报告",
        	url: rootUrl + "/test"
        }, {
        	displayName: "指标报告",
        	url: rootUrl + "/kpi"
        }, {
        	displayName: "精确覆盖",
        	url: rootUrl + "/precise"
        }
    ];
});