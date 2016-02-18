angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function($http, appUrlService) {
        return {
        	initializeCities: function (city) {
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function(result) {
                    city.options = result;
                    city.selected = result[0];
                });
            }
        };
    });