angular.module('kpi.import', ['myApp.url'])
    .factory('kpiImportService', function($q, $http, appUrlService) {
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
            }
        };
    });