angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function ($q, $http, appUrlService) {
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
    })
    .factory('appFormatService', function() {
        return {
            getDate: function (strDate) {
                var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/,
                    function (a) { return parseInt(a, 10) - 1; }).match(/\d+/g) + ')');
                return date;
            }
        }
    });