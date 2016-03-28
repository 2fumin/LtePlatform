angular.module('college', ['myApp.url'])
    .factory('collegeService', function($q, $http, appUrlService) {
        return {
            queryNames: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CollegeNames')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryStats: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CollegeStat')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryRegion: function (id) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CollegeRegion/' + id)
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryENodebs: function (name) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CollegeENodeb/'),
                    params: {
                        collegeName: name
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            }
        }
    });