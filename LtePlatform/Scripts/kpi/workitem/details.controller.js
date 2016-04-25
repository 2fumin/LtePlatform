app.controller("kpi.workitem.details", function ($scope, $http, $routeParams, workitemService) {
    $scope.detailsView = "none";
    
    $scope.dialogTitle = "工单网元信息";
    $scope.updatePlatformInfo = function() {
        $scope.platformInfos = [];
        var comments = $scope.currentView.comments;
        var fields = comments.split('[');
        if (fields.length > 1) {
            for (var j = 1; j < fields.length; j++) {
                var subFields = fields[j].split(']');
                $scope.platformInfos.push({
                    time: subFields[0],
                    contents: subFields[1]
                });
            }
        }
    };
    if ($scope.currentView === undefined) {
        workitemService.querySingleItem($routeParams.number).then(function(result) {
            $scope.currentView = result;
            $scope.updatePlatformInfo();
        });
    } else {
        for (var i = 0; i < $scope.viewData.items.length; i++) {
            if ($scope.viewData.items[i].serialNumber === $routeParams.number) {
                $scope.currentView = $scope.viewData.items[i];
                $scope.updatePlatformInfo();
                break;
            }
        }
    }

});