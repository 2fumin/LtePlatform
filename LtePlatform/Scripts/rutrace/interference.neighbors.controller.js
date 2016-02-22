app.controller("rutrace.interference.neighbors", function ($scope, $http, $uibModal, $log, neighborService) {
    $scope.match = function (candidate) {
        var center = $scope.topStat.current;
        neighborService.
        $http({
            method: 'GET',
            url: appUrlService.getApiUrl('Cell'),
            params: {
                'eNodebId': center.cellId,
                'sectorId': center.sectorId,
                'pci': candidate.destPci
            }
        }).success(function(result) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/Rutrace/Interference/MatchCellDialog.html',
                controller: 'ModalInstanceCtrl',
                size: 'lg',
                resolve: {
                    dialogTitle: function() {
                        return center.eNodebName + "-" + center.sectorId + "的邻区PCI=" + candidate.destPci + "的可能小区";
                    },
                    candidateNeighbors: function () {
                        return result;
                    },
                    currentCell: function() {
                        return center;
                    }
                }
            });

            modalInstance.result.then(function (nearestCell) {
                $scope.matchNearest(nearestCell, candidate, center);
            }, function() {
                $log.info('Modal dismissed at: ' + new Date());
            });

            
        });
    };

    $scope.matchNearest = function (nearestCell, currentNeighbor, center) {
        $http.put(appUrlService.getApiUrl('NearestPciCell'), {
            cellId: center.cellId,
            sectorId: center.sectorId,
            pci: currentNeighbor.destPci,
            nearestCellId: nearestCell.eNodebId,
            nearestSectorId: nearestCell.sectorId
        }).success(function() {
            currentNeighbor.neighborCellName = nearestCell.eNodebName + "-" + nearestCell.sectorId;
        });
    };
});

app.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, geometryService,
    dialogTitle, candidateNeighbors, currentCell) {
    $scope.pciNeighbors = [];
    $scope.indoorConsidered = false;
    $scope.distanceOrder = "distance";
    $scope.dialogTitle = dialogTitle;
    $scope.candidateNeighbors = candidateNeighbors;
    $scope.currentCell = currentCell;

    var minDistance = 10000;
    for (var i = 0; i < $scope.candidateNeighbors.length; i++) {
        var neighbor = $scope.candidateNeighbors[i];
        neighbor.distance = geometryService.getDistance($scope.currentCell.lattitute, $scope.currentCell.longtitute,
            neighbor.lattitute, neighbor.longtitute);
        if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
            minDistance = neighbor.distance;
            $scope.nearestCell = neighbor;
        }
        $scope.pciNeighbors.push(neighbor);
    }

    $scope.ok = function() {
        $uibModalInstance.close($scope.nearestCell);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});