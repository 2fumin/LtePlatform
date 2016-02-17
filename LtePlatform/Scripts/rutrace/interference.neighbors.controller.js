app.controller("rutrace.interference.neighbors", function ($scope, $http, appUrlService) {
	$scope.pciNeighbors = [];
	$scope.distanceOrder = "distance";
	$scope.indoorConsidered = false;
	$scope.nearestCell = {};
    $scope.currentNeighbor = {};

    $scope.match = function (cell) {
		$http({
			method: 'GET',
			url: appUrlService.getApiUrl('Cell'),
			params: {
				'eNodebId': $scope.currentCell.cellId,
				'sectorId': $scope.currentCell.sectorId,
				'pci': cell.destPci
			}
		}).success(function (result) {
		    $scope.currentNeighbor = cell;
			$scope.pciNeighbors = [];
			$scope.dialogTitle = $scope.currentCell.eNodebName + "-" + $scope.currentCell.sectorId + "的邻区PCI=" + cell.destPci + "的可能小区";
			$(".modal").modal("show");
		    var minDistance = 10000;
			for (var i = 0; i < result.length; i++) {
				var neighbor = result[i];
				neighbor.distance = getDistance($scope.currentCell.lattitute, $scope.currentCell.longtitute, neighbor.lattitute, neighbor.longtitute);
				if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
					minDistance = neighbor.distance;
				    $scope.nearestCell = neighbor;
				}
				$scope.pciNeighbors.push(neighbor);
			}
		});
    };

    $scope.matchNearest = function() {
    	
    	$(".modal").modal("hide");
        $http.put(appUrlService.getApiUrl('NearestPciCell'), {
            cellId: $scope.currentCell.cellId,
            sectorId: $scope.currentCell.sectorId,
            pci: $scope.currentNeighbor.destPci,
            nearestCellId: $scope.nearestCell.eNodebId,
            nearestSectorId: $scope.nearestCell.sectorId
        }).success(function() {
			$scope.currentNeighbor.neighborCellName = $scope.nearestCell.eNodebName + "-" + $scope.nearestCell.sectorId;
        });
    };
})