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

        serviceInstance.dumpMongo=function(actionUrl, progressInfo, begin, end) {
            var self = serviceInstance;
            if (progressInfo.dumpCells.length === 0) return;
            var index = 0;
            var cell = progressInfo.dumpCells[index];
            $http.post(actionUrl, {
                pciCell: cell,
                begin: begin,
                end: end
            }).success(function(result) {
                progressInfo.cellInfo = cell.eNodebId + '-' + cell.sectorId + '-' + cell.pci + ': ' + result;
            })
        }

        return serviceInstance;
    });