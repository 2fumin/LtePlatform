angular.module('myApp.authorize', ['myApp.url'])
    .factory('authorizeService', function($q, $http, appUrlService) {
        return {
            queryCurrentUserInfo: function() {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CurrentUser'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryAllUsers: function() {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('ApplicationUsers'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            updateRoleList: function() {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('ApplicationRoles'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            addRole: function(name) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('ApplicationRoles'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        roleName: name,
                        action: "create"
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            deleteRole: function (name) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('ApplicationRoles'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        roleName: name,
                        action: "delete"
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            changePassword: function(input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/ChangePassword',
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    data: input
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            addPhoneNumber: function(input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/AddPhoneNumber',
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    data: input
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            verifyPhoneNumber: function (input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/VerifyPhoneNumber',
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    data: input
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