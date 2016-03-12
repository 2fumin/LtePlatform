angular.module('myApp.dumpInterference', ['myApp.url'])
    .factory('dumpProgress', function($http, appUrlService) {
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

        serviceInstance.dumpMongo = function (progressInfo, begin, end, index, step) {
            var self = serviceInstance;
            if (progressInfo.dumpCells.length < index + 1) return;
            var cell = progressInfo.dumpCells[index];
            $http.post(appUrlService.getApiUrl('DumpInterference'), {
                pciCell: cell,
                begin: begin,
                end: end
            }).success(function(result) {
                progressInfo.cellInfo = cell.eNodebId + '-' + cell.sectorId + '-' + cell.pci + ': ' + result;
                progressInfo.totalSuccessItems = progressInfo.totalSuccessItems + 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.dumpCells.length) {
                    self.dumpMongo(progressInfo, begin, end, index + step, step);
                } else {
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            }).error(function() {
                progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                progressInfo.cellInfo = cell.eNodebId + '-' + cell.sectorId + '-' + cell.pci + ': Fail!!!';
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.dumpCells.length) {
                    self.dumpMongo(progressInfo, begin, end, index + step, step);
                } else {
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            });
        };

        serviceInstance.resetProgress = function (progressInfo, begin, end) {
            $http({
                method: 'GET',
                url: appUrlService.getApiUrl('DumpInterference'),
                params: {
                    'begin': begin,
                    'end': end
                }
            }).success(function (result) {
                progressInfo.dumpCells = result;
                progressInfo.totalFailItems = 0;
                progressInfo.totalSuccessItems = 0;
            });
        };

        return serviceInstance;
    });