angular.module('myApp.authorize', ['myApp.url'])
    .factory('authorizeService', function($q, $http, appUrlService) {
        return {
            queryCurrentUserInfo: function() {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CurrentUser')
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
                    url: appUrlService.getApiUrl('ApplicationUsers')
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
                    url: appUrlService.getApiUrl('ApplicationRoles')
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
                    data: input
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            forgotPassword: function (input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/ForgotPassword',
                    data: input
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            resetPassword: function (input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/ResetPassword',
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
                    data: input
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            removePhoneNumber: function () {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/RemovePhoneNumber'
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            confirmEmail: function (input) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Manage/ConfirmEmail',
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