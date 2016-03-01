app.controller("kpi.workitem", function ($scope, workitemService) {
    $scope.page.title = "工单总览";
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

    $scope.totalItems = 0;
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

    $scope.updateWorkItemTable = function() {
        workitemService.queryTotalPages($scope.currentState.name, $scope.currentType.name,
            $scope.itemsPerPage.value).then(function(result) {
            $scope.totalItems = result;
            $scope.currentPage = 1;
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
        $scope.query();
    });

    $scope.$watch('itemsPerPage', function(newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        if ($scope.currentPage > Math.ceil($scope.totalItems / newValue)) {
            $scope.currentPage = Math.ceil($scope.totalItems / newValue);
        }
        $scope.query();
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

    $scope.updateWorkItemTable();
});