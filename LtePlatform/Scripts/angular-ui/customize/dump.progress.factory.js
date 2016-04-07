angular.module('myApp.dumpInterference', ['myApp.url'])
    .factory('dumpProgress', function ($http, $q, appUrlService, appFormatService) {
        var serviceInstance = {};

        serviceInstance.clear = function(progressInfo) {
            $http.delete(appUrlService.getApiUrl('DumpInterference')).success(function () {
                progressInfo.totalDumpItems = 0;
                progressInfo.totalSuccessItems = 0;
                progressInfo.totalFailItems = 0;
            });
        };
        
        serviceInstance.dump = function (actionUrl, progressInfo) {
            var self = serviceInstance;
            $http.put(actionUrl).success(function(result) {
                if (result === true) {
                    progressInfo.totalSuccessItems = progressInfo.totalSuccessItems + 1;
                } else {
                    progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                }
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            }).error(function() {
                progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            });
        };

        serviceInstance.dumpMongo = function (stat) {
            var deferred = $q.defer();
            $http.post(appUrlService.getApiUrl('DumpInterference'), stat)
                .success(function (result) {
                deferred.resolve(result);
            })
                .error(function (reason) {
                    deferred.reject(reason);
                });
            return deferred.promise;
        };

        serviceInstance.resetProgress = function (begin, end) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('DumpInterference'),
                params: {
                    'begin': begin,
                    'end': end
                }
            }).success(function (result) {
                deferred.resolve(result);
            })
            .error(function (reason) {
                deferred.reject(reason);
            });
            return deferred.promise;
        };

        serviceInstance.queryExistedItems = function(eNodebId, sectorId, date) {
            var deferred = $q.defer();
            $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('DumpInterference'),
                    params: {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        date: appFormatService.getDateString(date, 'yyyy-MM-dd')
                    }
                }).success(function(result) {
                    deferred.resolve({ date: date, value: result });
                })
                .error(function(reason) {
                    deferred.reject(reason);
                });
            return deferred.promise;
        };

        serviceInstance.queryMongoItems = function (eNodebId, pci, date) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('DumpInterference'),
                params: {
                    eNodebId: eNodebId,
                    pci: pci,
                    date: date
                }
            }).success(function (result) {
                deferred.resolve({date: date, value: result});
            })
                .error(function (reason) {
                    deferred.reject(reason);
                });
            return deferred.promise;
        };

        return serviceInstance;
    });