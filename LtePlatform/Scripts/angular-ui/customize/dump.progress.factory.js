angular.module('myApp.Services', [])
    .factory('dumpProgress', function($http) {
        var serviceInstance = {};

        serviceInstance.clear = function(actionUrl, progressInfo) {
            $http.delete(actionUrl).success(function() {
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

        serviceInstance.dumpMongo = function (actionUrl, progressInfo, begin, end, index) {
            var self = serviceInstance;
            if (progressInfo.dumpCells.length === 0) return;
            var cell = progressInfo.dumpCells[index];
            $http.post(actionUrl, {
                pciCell: cell,
                begin: begin,
                end: end
            }).success(function(result) {
                progressInfo.cellInfo = cell.eNodebId + '-' + cell.sectorId + '-' + cell.pci + ': ' + result;
                progressInfo.totalSuccessItems = progressInfo.totalSuccessItems + 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.dumpCells.length) {
                    self.dumpMongo(actionUrl, progressInfo, begin, end, index + 1);
                } else {
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            }).error(function() {
                progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                progressInfo.cellInfo = cell.eNodebId + '-' + cell.sectorId + '-' + cell.pci + ': Fail!!!';
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.dumpCells.length) {
                    self.dumpMongo(actionUrl, progressInfo, begin, end, index + 1);
                } else {
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            });
        };

        return serviceInstance;
    });