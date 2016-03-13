app.controller('cdmaCell.vanished', function ($scope, networkElementService) {
    $scope.vanishedCdmaCells = [];
    var data = $scope.importData.vanishedCdmaCellIds;
    for (var i = 0; i < data.length; i++) {
        networkElementService.queryCdmaCellInfo(data[i].cellId, data[i].sectorId).then(function (result) {
            $scope.vanishedCdmaCells.push(result);
        });
    }

});