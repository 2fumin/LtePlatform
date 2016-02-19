angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function($q, $http, appUrlService) {
        return {
            initializeCities: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function (result) {
                    deferred.resolve({
                        options: result,
                        selected: result[0]
                    });
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            }
        };
    });