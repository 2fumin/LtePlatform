app.controller("homeController", function($scope, $http) {
    $scope.menuItems = [
    {
        title: "小区覆盖仿真模拟",
        comments: "根据各小区的工程参数模拟覆盖范围，主要覆盖指标（RSRP、SINR）进行分析和呈现",
        buttonName: "开始仿真",
        url: "/Evaluation/RegionDef"
    }, {
        title: "精确覆盖专题分析",
        comments: "综合分析后台指标、MR、路测信令和小区跟踪数据，挖掘小区的重叠覆盖、过覆盖等问题，对精确覆盖的效果进行模拟，并在百度地图上呈现。",
        buttonName: "精确覆盖分析",
        url: "/Rutrace/Import"
    }, {
        title: "校园网专题优化",
        comments: "校园网专项优化，包括数据管理、指标分析、支撑工作管理和校园网覆盖呈现",
        buttonName: "校园网一览",
        url: "/College/Map"
    }, {
        title: "指标监控分析",
        comments: "对日常指标的监控、精确覆盖率指标分析和地理化呈现",
        buttonName: "日常指标监控",
        url: "/Kpi"
    }, {
        title: "小区基础信息",
        comments: "全网基站、小区列表和地理化显示、对全网的基站按照基站名称、地址等信息进行查询，并进行个别基站小区的增删、修改信息的操作",
        buttonName: "LTE小区列表",
        url: "/Parameters/List"
    }, {
        title: "DT基础信息",
        comments: "按照区域或专题查看已导入的DT基础信息",
        buttonName: "DT数据查看",
        url: "/Dt/List"
    }];

    $scope.dataModel = new AppDataModel();
    $scope.dataModel.initializeAuthorization();
    $http({
        method: 'get',
        url: $scope.dataModel.userInfoUrl,
        headers: {
            'Authorization': 'Bearer ' + $scope.dataModel.getAccessToken()
        }
    }).success(function(data) {
        $scope.currentUser = data;
    });
});
