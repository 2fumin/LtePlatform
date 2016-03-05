app.controller("homeController", function ($scope, $http, appUrlService) {
    $scope.menuItems = [
    {
        title: "小区覆盖仿真模拟",
        comments: "根据各小区的工程参数模拟覆盖范围，主要覆盖指标（RSRP、SINR）进行分析和呈现",
        buttonName: "开始仿真",
        url: "/Evaluation/RegionDef"
    }, {
        title: "精确覆盖专题分析",
        comments: "综合分析后台指标、MR、路测信令和小区跟踪数据，挖掘小区的重叠覆盖、过覆盖等问题，对精确覆盖的效果进行模拟，并在百度地图上呈现。",
        buttonName: "精确覆盖",
        url: "/Rutrace"
    }, {
        title: "校园网专题优化",
        comments: "校园网专项优化，包括数据管理、指标分析、支撑工作管理和校园网覆盖呈现",
        buttonName: "校园网一览",
        url: "/College/Map"
    }, {
        title: "传统指标监控",
        comments: "对传统指标（主要是2G和3G）的监控、分析和地理化呈现",
        buttonName: "传统指标",
        url: "/Kpi"
    }, {
        title: "工单监控分析",
        comments: "对接本部优化部4G网优平台，实现对日常工单的监控和分析",
        buttonName: "工单监控",
        url: "/Kpi/WorkItem"
    }, {
        title: "小区基础信息",
        comments: "全网LTE和CDMA基站、小区列表和地理化显示、对全网的基站按照基站名称、地址等信息进行查询，并进行个别基站小区的增删、修改信息的操作",
        buttonName: "基础信息",
        url: "/Parameters/List"
    }, {
        title: "DT基础信息",
        comments: "按照区域或专题查看已导入的DT基础信息",
        buttonName: "DT数据查看",
        url: "/Dt/List"
    }];

    $http({
        method: 'get',
        url: appUrlService.userInfoUrl,
        headers: {
            'Authorization': 'Bearer ' + appUrlService.getAccessToken()
        }
    }).success(function(data) {
        $scope.currentUser = data;
    });

    $scope.status = {
        isopen: false
    };

});
