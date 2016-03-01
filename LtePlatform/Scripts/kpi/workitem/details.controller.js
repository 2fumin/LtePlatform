app.controller("kpi.workitem.details", function ($scope, $http, $routeParams) {
    $scope.detailsView = "none";
    
    $scope.btsDetails = {};
    $scope.lteCellDetails = {};
    $scope.cdmaCellDetails = {};
    $scope.dialogTitle = "工单网元信息";
    for (var i = 0; i < $scope.viewData.items.length; i++) {
        if ($scope.viewData.items[i].serialNumber === $routeParams.number) {
            $scope.currentView = $scope.viewData.items[i];
            break;
        }
    }
});