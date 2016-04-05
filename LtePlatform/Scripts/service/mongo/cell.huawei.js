angular.module('huawei.mongo.parameters', ['myApp.url'])
    .factory('cellHuaweiMongoService', function($q, $http, appUrlService) {
        return {
            queryCellParameters: function(eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('CellHuaweiMongo'),
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
            },
            queryLocalCellDef: function (eNodebId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CellHuaweiMongo'),
                    params: {
                        eNodebId: eNodebId
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
    })
    .filter("dbStep32", function() {
        var dict = [
            -24, -22, -20, -18, -16, -14, -12, -10, -8, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 32 ? dict[input] : input;
        };
    });