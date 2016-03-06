angular.module('kpi.import', ['myApp.url'])
    .factory('preciseImportService', function($q, $http, appUrlService) {
        return {
            queryDumpHistroy: function (beginDate, endDate) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseImport'),
                    params: {
                        begin: beginDate,
                        end: endDate
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryTotalDumpItems: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseImport')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryTownPreciseViews: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TownPreciseImport')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            clearImportItems: function() {
                var deferred = $q.defer();
                $http.delete(appUrlService.getApiUrl('PreciseImport')).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    });