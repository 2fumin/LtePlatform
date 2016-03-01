app.controller("kpi.workitem", function ($scope, workitemService) {
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

    $scope.chartView = "initial";

    $scope.updateWorkItemTable = function () {
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

    $scope.updateSectorIds = function() {
        workitemService.updateSectorIds().then(function (result) {
            $scope.page.messages.push({
                contents: "一共更新扇区编号：" + result + "条",
                type: "success"
            });
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
        $scope.updateWorkItemTable();
    });

    $scope.$watch('currentState', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        $scope.updateWorkItemTable();
    });

    $scope.$watch('currentType', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        $scope.updateWorkItemTable();
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

    $scope.updateWorkItemTable();
});