app.controller('bts.vanished', function ($scope, networkElementService) {
    $scope.vanishedBtss = [];
    var data = $scope.importData.vanishedBtsIds;
    for (var i = 0; i < data.length; i++) {
        networkElementService.queryBtsInfo(data[i]).then(function (result) {
            $scope.vanishedBtss.push(result);
        });
    }

});