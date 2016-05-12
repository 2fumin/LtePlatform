angular.module('myApp.dumpInterference', ['myApp.url', 'myApp.parameters'])
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

        serviceInstance.dumpCellStat = function (stat) {
            var deferred = $q.defer();
            $http.post(appUrlService.getApiUrl('DumpCellStat'), stat)
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

        serviceInstance.queryMongoCellStat = function (eNodebId, pci, date) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('DumpCellStat'),
                params: {
                    eNodebId: eNodebId,
                    pci: pci,
                    date: date
                }
            }).success(function (result) {
                deferred.resolve({ date: date, value: result });
            })
                .error(function (reason) {
                    deferred.reject(reason);
                });
            return deferred.promise;
        };

        serviceInstance.queryNeighborMongoItem = function (eNodebId, pci, neighborPci, date) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('DumpInterference'),
                params: {
                    eNodebId: eNodebId,
                    pci: pci,
                    neighborPci: neighborPci,
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
    })
    .factory('dumpPreciseService', function (dumpProgress, neighborService) {
        var serviceInstance = {};
        serviceInstance.dumpRecords = function(records, index, eNodebId, sectorId, queryFunc) {
            if (index < records.length) {
                var stat = records[index];
                stat.sectorId = sectorId;
                neighborService.querySystemNeighborCell(eNodebId, sectorId, stat.destPci).then(function(neighbor) {
                    if (neighbor) {
                        stat.destENodebId = neighbor.nearestCellId;
                        stat.destSectorId = neighbor.nearestSectorId;
                    }
                    dumpProgress.dumpMongo(stat).then(function() {
                        serviceInstance.dumpRecords(records, index + 1, eNodebId, sectorId, queryFunc);
                    });
                });
            } else {
                queryFunc();
            }
        };
        serviceInstance.dumpAllRecords = function (records, outerIndex, innerIndex, eNodebId, sectorId, queryFunc) {
            if (outerIndex >= records.length) {
                if (queryFunc !== undefined)
                    queryFunc();
            } else {
                var subRecord = records[outerIndex];
                if (subRecord.existedRecords < subRecord.mongoRecords.length && innerIndex < subRecord.mongoRecords.length) {
                    var stat = subRecord.mongoRecords[innerIndex];
                    stat.sectorId = sectorId;
                    neighborService.querySystemNeighborCell(eNodebId, sectorId, stat.destPci).then(function(neighbor) {
                        if (neighbor) {
                            stat.destENodebId = neighbor.nearestCellId;
                            stat.destSectorId = neighbor.nearestSectorId;
                        }
                        dumpProgress.dumpMongo(stat).then(function() {
                            serviceInstance.dumpAllRecords(records, outerIndex, innerIndex + 1, eNodebId, sectorId, queryFunc);
                        });
                    });
                } else
                    serviceInstance.dumpAllRecords(records, outerIndex + 1, 0, eNodebId, sectorId, queryFunc);
            }

        };
        serviceInstance.dumpDateSpanSingleNeighborRecords = function(eNodebId, sectorId, pci, destENodebId, destSectorId, destPci, date, end) {
            if (date < end) {
                dumpProgress.queryNeighborMongoItem(eNodebId, pci, destPci, date).then(function (result) {
                    var stat = result.value;
                    var nextDate = result.date;
                    nextDate.setDate(nextDate.getDate() + 1);
                    if (stat) {
                        stat.sectorId = sectorId;
                        stat.destENodebId = destENodebId;
                        stat.destSectorId = destSectorId;
                        dumpProgress.dumpMongo(stat).then(function() {
                            serviceInstance.dumpDateSpanSingleNeighborRecords(eNodebId, sectorId, pci, destENodebId, destSectorId, destPci, nextDate, end);
                        });
                    } else {
                        serviceInstance.dumpDateSpanSingleNeighborRecords(eNodebId, sectorId, pci, destENodebId, destSectorId, destPci, nextDate, end);
                    }
                });
            }
        };

        return serviceInstance;
    });