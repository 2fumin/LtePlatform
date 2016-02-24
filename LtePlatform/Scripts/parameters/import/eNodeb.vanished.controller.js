app.controller('eNodeb.vanished', function($scope, networkElementService) {
    $scope.vanishedENodebs = [];
    var data = $scope.importData.vanishedENodebIds;
    for (var i = 0; i < data.length; i++) {
        networkElementService.queryENodebInfo(data[i]).then(function(result) {
            $scope.vanishedENodebs.push(result);
        });
    }

});