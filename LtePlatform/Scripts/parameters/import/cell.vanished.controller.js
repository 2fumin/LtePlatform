app.controller('cell.vanished', function($scope, networkElementService) {
    $scope.vanishedCells = [];
    var data = $scope.importData.vanishedCellIds;
    for (var i = 0; i < data.length; i++) {
        networkElementService.queryCellInfo(data[i].cellId, data[i].sectorId).then(function(result) {
            $scope.vanishedCells.push(result);
        });
    }

});