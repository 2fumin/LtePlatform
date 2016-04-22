app.config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Test/Simple/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "Index.html",
                    controller: "qunit.index"
                })
                .when('/method/:name', {
                    templateUrl: viewDir + "ApiMethod.html",
                    controller: "api.method"
                })
                .when('/api/:apiId/:method', {
                    templateUrl: viewDir + "Api.html",
                    controller: "api.details"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/TestPage/QUintTest/Help#";
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "",
            introduction: ""
        };
        $scope.menuItems = [
        {
            displayName: "QUnit官网案例测试",
            isActive: true,
            subItems: [
            {
                displayName: "QUnit例子",
                url: "/",
                tooltip: "综合分析后台指标、MR、路测信令和小区跟踪数据，挖掘小区的重叠覆盖、过覆盖等问题，对精确覆盖的效果进行模拟，并在百度地图上呈现。"
            }, {
                displayName: "Main Test",
                url: "/Main",
                tooltip: "根据各小区的工程参数模拟覆盖范围，主要覆盖指标（RSRP、SINR）进行分析和呈现"
            }, {
                displayName: "Legacy Markup",
                url: "/LegacyMarkup",
                tooltip: "按照区域或专题查看已导入的DT基础信息"
            }, {
                displayName: "No QUnit Markup",
                url: "/NoQUnitMarkup"
            }, {
                displayName: "Single Test Id",
                url: "/SingleTestId"
            }, {
                displayName: "Auto Start",
                url: "/AutoStart"
            }, {
                displayName: "Headless",
                url: "/Headless"
            }, {
                displayName: "Logs",
                url: "/Logs"
            }, {
                displayName: "Only",
                url: "/Only"
            }]
        }, {
            displayName: "CoffeeScript脚本测试",
            isActive: false,
            subItems: [{
                displayName: "Hotseat 5x5",
                url: "/TestPage/CoffeeScript/Hotseat",
                tooltip: "全网LTE和CDMA基站、小区列表和地理化显示、对全网的基站按照基站名称、地址等信息进行查询，并进行个别基站小区的增删、修改信息的操作"
            }]
        }, {
            displayName: "WebApi测试",
            isActive: false,
            subItems: [{
                displayName: "工参上传测试",
                url: "/TestPage/WebApiTest/BasicPost",
                tooltip: "全网LTE和CDMA基站、小区列表和地理化显示、对全网的基站按照基站名称、地址等信息进行查询，并进行个别基站小区的增删、修改信息的操作"
            }, {
                displayName: "简单类型测试",
                url: "/TestPage/WebApiTest/SimpleType",
                tooltip: "对传统指标（主要是2G和3G）的监控、分析和地理化呈现"
            }, {
                displayName: "Html5csv测试",
                url: "/TestPage/WebApiTest/Html5Test",
                tooltip: "对接本部优化部4G网优平台，实现对日常工单的监控和分析"
            }, {
                displayName: "Html5csv测试-2",
                url: "/TestPage/WebApiTest/Html5PostTest",
                tooltip: "校园网专项优化，包括数据管理、指标分析、支撑工作管理和校园网覆盖呈现"
            }]
        }
        ];
    });