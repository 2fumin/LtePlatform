angular.module('handoff.parameters', ['myApp.url'])
    .factory('intraFreqHoService', function($q, $http, appUrlService) {
        return {
            queryENodebParameters: function(eNodebId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('IntraFreqHo'),
                        params: {
                            eNodebId: eNodebId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellParameters: function(eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('IntraFreqHo'),
                        params: {
                            eNodebId: eNodebId,
                            sectorId: sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    })
    .factory('interFreqHoService', function($q, $http, appUrlService) {
        return {
            queryENodebParameters: function(eNodebId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('InterFreqHo'),
                        params: {
                            eNodebId: eNodebId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellParameters: function (eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('InterFreqHo'),
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
            }
        };
    });