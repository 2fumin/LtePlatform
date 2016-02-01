app.controller("kpi.workitem.details", function ($scope, $http) {
    $scope.detailsView = "none";
    $scope.eNodebDetails = {};
    $scope.btsDetails = {};
    $scope.lteCellDetails = {};
    $scope.cdmaCellDetails = {};
    $scope.dialogTitle = "工单网元信息";

    $scope.queryBtsInfo = function () {
        var eNodebId = $scope.currentView.eNodebId;
        if (eNodebId > 10000) {
            $scope.detailsView = "eNodeb";
            $http({
                method: 'GET',
                url: $scope.dataModel.eNodebUrl,
                params: {
                    'eNodebId': eNodebId
                }
            }).success(function (result) {
                $scope.eNodebDetails = result;
                $scope.dialogTitle = "工单网元信息:" + result.name;
            });
        } else {
            $scope.detailsView = "bts";
            $http({
                method: 'GET',
                url: $scope.dataModel.btsUrl,
                params: {
                    'btsId': eNodebId
                }
            }).success(function (result) {
                $scope.btsDetails = result;
                $scope.dialogTitle = "工单网元信息:" + result.name;
            });
        }
        $(".modal").modal("show");
    };

    $scope.queryCellInfo = function () {
        var eNodebId = $scope.currentView.eNodebId;
        var sectorId = $scope.currentView.sectorId;
        if (eNodebId > 10000) {
            $scope.detailsView = "lteCell";
            $http({
                method: 'GET',
                url: $scope.dataModel.cellUrl,
                params: {
                    'eNodebId': eNodebId,
                    'localSector': sectorId
                }
            }).success(function (result) {
                $scope.lteCellDetails = result;
                $scope.dialogTitle = "工单网元信息:" + result.eNodebName + "-" + result.sectorId;
            });
        } else {
            $scope.detailsView = "cdmaCell";
            $http({
                method: 'GET',
                url: $scope.dataModel.cdmaCellUrl,
                params: {
                    'btsId': eNodebId,
                    'sectorId': sectorId
                }
            }).success(function (result) {
                $scope.cdmaCellDetails = result;
                $scope.dialogTitle = "工单网元信息:" + result.btsName + "-" + result.sectorId;
            });
        }
        $(".modal").modal("show");
    };


});