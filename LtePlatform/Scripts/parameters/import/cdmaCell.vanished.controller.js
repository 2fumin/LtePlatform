app.controller('cdmaCell.vanished', function ($scope, networkElementService) {
    $scope.vanishedCdmaCells = [];
    var data = $scope.importData.vanishedCdmaCellIds;
    for (var i = 0; i < data.length; i++) {
        networkElementService.queryCdmaCellInfoWithType(data[i].cellId, data[i].sectorId, data[i].cellType).then(function (result) {
            $scope.vanishedCdmaCells.push(result);
        });
    }

});