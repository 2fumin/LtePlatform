angular.module('kpi.workitem', ['myApp.url'])
    .factory('workitemService', function($q, $http, appUrlService) {
        return {
            queryWithPaging: function(state, type, itemsPerPage, page) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        'statCondition': state,
                        'typeCondition': type,
                        'itemsPerPage': itemsPerPage,
                        'page': page
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryTotalPages: function(state, type, itemsPerPage) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        'statCondition': state,
                        'typeCondition': type,
                        'itemsPerPage': itemsPerPage
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