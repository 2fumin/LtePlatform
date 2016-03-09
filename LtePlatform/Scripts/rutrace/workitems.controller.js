app.controller("rutrace.workitems", function ($scope, $routeParams, workitemService) {
    $scope.page.title = "TOP小区工单历史";

    workitemService.queryByCellId($routeParams.cellId, $routeParams.sectorId).then(function(result) {
        $scope.viewItems = result;
        $scope.viewData.workItems = result;
        if (result.length > 0) {
            $scope.currentView = result[0];
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
        }
    });
});