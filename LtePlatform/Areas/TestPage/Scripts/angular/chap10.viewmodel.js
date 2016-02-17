app.config([
    '$routeProvider', function ($routeProvider) {
        var viewDir = "/appViews/Test/Chapter10/";
        $routeProvider
            .when('/', {
                templateUrl: viewDir + "Links.html",
                controller: "LinksController"
            })
            .when('/date', {
                templateUrl: viewDir + "DateTimeTest.html",
                controller: "DateController"
            })
            .when('/dateparser', {
                templateUrl: viewDir + "demo.dateparser.html",
                controller: "DateParserDemoCtrl"
            })
            .when('/datepicker', {
                templateUrl: viewDir + "demo.datepicker.html",
                controller: "DatepickerDemoCtrl"
            })
            .when('/accordion', {
                templateUrl: viewDir + "demo.accordion.html",
                controller: "AccordionDemoCtrl"
            })
            .when('/alert', {
                templateUrl: viewDir + "demo.alert.html",
                controller: "AlertDemoCtrl"
            })
            .when('/buttons', {
                templateUrl: viewDir + "demo.buttons.html",
                controller: "ButtonsCtrl"
            })
            .when('/carousel', {
                templateUrl: viewDir + "demo.carousel.html",
                controller: "CarouselDemoCtrl"
            })
            .when('/collapse', {
                templateUrl: viewDir + "demo.collapse.html",
                controller: "CollapseDemoCtrl"
            })
            .when('/dropdown', {
                templateUrl: viewDir + "demo.dropdown.html",
                controller: "DropdownCtrl"
            })
            .when('/modal', {
                templateUrl: viewDir + "demo.modal.html",
                controller: "ModalDemoCtrl"
            })
            .when('/pager', {
                templateUrl: viewDir + "demo.pager.html",
                controller: "PagerDemoCtrl"
            })
            .when('/pagination', {
                templateUrl: viewDir + "demo.pagination.html",
                controller: "PaginationDemoCtrl"
            })
            .when('/popover', {
                templateUrl: viewDir + "demo.popover.html",
                controller: "PopoverDemoCtrl"
            })
            .when('/progress', {
                templateUrl: viewDir + "demo.progress.html",
                controller: "ProgressDemoCtrl"
            })
            .otherwise({
                redirectTo: '/'
            });
    }
]);

app.controller("chap10.root", function ($scope) {
    $scope.pageTitle = "Chapter10Ari";
    var rootPath = "/TestPage/AngularTest/Chapter10Ari#";
    $scope.sectionItems = [
    {
        name: "Links",
        displayName: "指令测试",
        url: rootPath + '/'
    }, {
        displayName: "日期控件",
        items: [
        {
            url: rootPath + "/date",
            displayName: "自定义"
        }, {
            url: rootPath + "/dateparser",
            displayName: "日期解析"
        }, {
            url: rootPath + "/datepicker",
            displayName: "复杂控件"
        }]
    }, {
        displayName: "控件实例I",
        items: [
        {
            url: rootPath + "/accordion",
            displayName: "手风琴"
        }, {
            url: rootPath + "/alert",
            displayName: "警告框"
        }, {
            url: rootPath + "/buttons",
            displayName: "按钮"
        }, {
            url: rootPath + "/collapse",
            displayName: "折叠"
        }, {
            url: rootPath + "/progress",
            displayName: "进度条"
        }]
    }, {
        displayName: "控件实例II",
        items: [{
            url: rootPath + "/carousel",
            displayName: "轮播"
        }, {
            url: rootPath + "/dropdown",
            displayName: "下拉列表"
        }, {
            url: rootPath + "/pager",
            displayName: "分页符"
        }, {
            url: rootPath + "/pagination",
            displayName: "分页"
        }]
    }, {
        displayName: "控件实例III",
        items: [{
            url: rootPath + "/modal",
            displayName: "模态框"
        }, {
            url: rootPath + "/popover",
            displayName: "弹出"
        }]
    }];
});

app.controller("LinksController", function($scope) {
    $scope.sectionTitle = "Links";
});
