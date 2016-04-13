angular.module('neighbor.mongo', ['myApp.url'])
    .factory('neighborMongoService', function($q, $http, appUrlService) {
        return {
            queryNeighbors: function (eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('NeighborCellMongo'),
                    params: {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryReverseNeighbors: function(destENodebId, destSectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('NeighborCellMongo'),
                    params: {
                        destENodebId: destENodebId,
                        destSectorId: destSectorId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    });