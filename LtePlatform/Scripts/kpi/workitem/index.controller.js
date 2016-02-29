﻿app.controller("kpi.workitem", function ($scope, $http, showPieChart, workitemService) {
    $scope.states = [
    {
        name: '未完成'
    }, {
        name: '全部'
    }];
    $scope.currentState = $scope.states[0];
    $scope.types = [
    {
        name: '全部'
    }, {
        name: '2/3G'
    }, {
        name: '4G'
    }];
    $scope.currentType = $scope.types[0];

    $scope.totalPages = 0;
    $scope.currentPage = 1;
    $scope.pageSizeSelection = [
    {
        value: 10
    }, {
        value: 15
    }, {
        value: 20
    }, {
        value: 30
    }, {
        value: 50
    }];
    $scope.itemsPerPage = $scope.pageSizeSelection[1];
    $scope.canGotoCurrentPage = false;
    $scope.currentView = {};
    $scope.updateNums = 0;

    $scope.chartView = "initial";

    $scope.updateWorkItemTable = function (items) {
        workitemService.queryWithPaging($scope.currentState.name, $scope.currentType.name,
            $scope.itemsPerPage.value).then(function (result) {
            $scope.totalPages = result;
            if ($scope.currentPage > result) {
                $scope.currentPage = result;
            }
            $scope.query();
        });
    };

    $scope.query = function () {
        workitemService.queryWithPaging($scope.currentState.name, $scope.currentType.name,
            $scope.itemsPerPage.value, $scope.currentPage).then(function(result) {
            $scope.viewData.items = result;
        });
    };

    $scope.showCharts = function () {
        if ($scope.chartView === 'initial') {
            $http.get($scope.dataModel.workItemUrl).success(function (result) {
                showPieChart.type(result, "#type-chart");
                showPieChart.state(result, "#state-chart");
            });
        }

        $scope.chartView = "chart";
    };

    $scope.updateSectorIds = function() {
        $http.put($scope.dataModel.workItemUrl).success(function(result) {
            $scope.updateNums = result;
        });
    };

    $scope.$watch('currentPage', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        if (newValue >= 1 && newValue <= $scope.totalPages) {
            $scope.canGotoCurrentPage = true;
        } else {
            $scope.canGotoCurrentPage = false;
            $scope.currentPage = 1;
        }
    });

    $scope.$watch('itemsPerPage', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        $scope.updateWorkItemTable($scope.itemsPerPage.value);
    });

    $scope.$watch('currentState', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        $scope.updateWorkItemTable($scope.itemsPerPage.value);
    });

    $scope.$watch('currentType', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        $scope.updateWorkItemTable($scope.itemsPerPage.value);
    });

    $scope.queryFirstPage = function () {
        $scope.currentPage = 1;
        $scope.query();
    };

    $scope.queryPrevPage = function () {
        $scope.currentPage = $scope.currentPage - 1;
        $scope.query();
    };

    $scope.queryNextPage = function () {
        $scope.currentPage = $scope.currentPage + 1;
        $scope.query();
    };

    $scope.queryLastPage = function () {
        $scope.currentPage = $scope.totalPages;
        $scope.query();
    };

    $scope.showDetails = function (data) {
        $scope.chartView = "details";
        $scope.currentView = data;
    };

    $scope.updateWorkItemTable($scope.itemsPerPage.value);
});