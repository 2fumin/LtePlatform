angular.module('myApp.parameters', ['myApp.url'])
    .factory('neighborService', function($q, $http, appUrlService) {
        return {
            queryCellNeighbors: function(cell) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('NearestPciCell'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        'cellId': cell.cellId,
                        'sectorId': cell.sectorId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            updateCellPci: function (cell) {
                var deferred = $q.defer();
                $http.post(appUrlService.getApiUrl('NearestPciCell'), cell)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            monitorNeighbors: function(cell) {
                var deferred = $q.defer();
                $http.post(appUrlService.getApiUrl('NeighborMonitor'), {
                        cellId: cell.nearestCellId,
                        sectorId: cell.nearestSectorId
                    })
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    });