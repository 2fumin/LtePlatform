app.controller("kpi.workitem", function($scope, $http) {
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
    $scope.workItemViews = [];
    $scope.canGotoCurrentPage = false;
    $scope.currentView = {};

    $scope.chartView = "initial";
    $scope.dataModel = new AppDataModel();

    $scope.updateWorkItemTable = function (items) {
        $http({
            method: 'GET',
            url: $scope.dataModel.workItemUrl,
            params: {
                'statCondition': $scope.currentState.name,
                'typeCondition': $scope.currentType.name,
                'itemsPerPage': items
            }
        }).success(function (result) {
            $scope.totalPages = result;
            if ($scope.currentPage > result) {
                $scope.currentPage = result;
            }
            $scope.query();
        });
    };

    $scope.query = function () {
        $http({
            method: 'GET',
            url: $scope.dataModel.workItemUrl,
            params: {
                'statCondition': $scope.currentState.name,
                'typeCondition': $scope.currentType.name,
                'itemsPerPage': $scope.itemsPerPage.value,
                'page': $scope.currentPage
            }
        }).success(function (result) {
            $scope.workItemViews = result;
        });
    };

    $scope.showCharts = function () {
        if ($scope.chartView === 'initial') {
            $http.get($scope.dataModel.workItemUrl).success(function (result) {
                showTypePieChart(result, "#type-chart");
                showStatePieChart(result, "#state-chart");
            });
        }

        $scope.chartView = "chart";
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