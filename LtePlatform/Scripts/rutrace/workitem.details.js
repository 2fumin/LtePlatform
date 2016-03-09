app.controller("workitem.details", function($scope, $http, $routeParams) {
    $scope.viewItems = $scope.viewData.workItems;
    for (var i = 0; i < $scope.viewItems.length; i++) {
        if ($scope.viewItems[i].serialNumber === $routeParams.number) {
            $scope.currentView = $scope.viewItems[i];
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
            break;
        }
    }
});