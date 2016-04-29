angular.module('kpi.workitem', ['myApp.url'])
    .factory('workitemService', function($q, $http, appUrlService) {
        return {
            queryWithPaging: function(state, type, itemsPerPage, page) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        'statCondition': state,
                        'typeCondition': type,
                        'itemsPerPage': itemsPerPage,
                        'page': page
                    },
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
            queryWithPagingByDistrict: function (state, type, district, itemsPerPage, page) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        statCondition: state,
                        typeCondition: type,
                        district: district,
                        itemsPerPage: itemsPerPage,
                        page: page
                    },
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
            queryTotalPages: function(state, type) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        'statCondition': state,
                        'typeCondition': type
                    },
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
            queryTotalPagesByDistrict: function (state, type, district) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        statCondition: state,
                        typeCondition: type,
                        district: district
                    },
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
            querySingleItem: function (serialNumber) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        serialNumber: serialNumber
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryChartData: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
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
            updateSectorIds: function() {
                var deferred = $q.defer();
                $http.put(appUrlService.getApiUrl('WorkItem')).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            feedback: function (message, serialNumber) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: appUrlService.getApiUrl('WorkItem'),
                    data: {
                        message: message,
                        serialNumber: serialNumber
                    },
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryByENodebId: function (eNodebId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        eNodebId: eNodebId
                    },
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
            queryByCellId: function (eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItem'),
                    params: {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    },
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
            queryCurrentMonth: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('WorkItemCurrentMonth')
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