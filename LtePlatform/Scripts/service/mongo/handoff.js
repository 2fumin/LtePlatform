angular.module('handoff.parameters', ['myApp.url'])
    .factory('intraFreqHoService', function($q, $http, appUrlService) {
        return {
            queryENodebParameters: function(eNodebId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('IntraFreqHo'),
                        params: {
                            eNodebId: eNodebId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellParameters: function(eNodebId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('IntraFreqHo'),
                        params: {
                            eNodebId: eNodebId,
                            sectorId: sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        };
    })
    .filter("halfDb", function() {
        return function(input) {
            return angular.isNumber(input) ? input / 2 : input;
        };
    })
    .filter("triggerQuantity", function(){
        var types=["RSRP", "RSRQ"];
        return function(input){
            return input===0||input===1?types[input]:input;
        };
    })
    .fileter("reportQuantity", function(){
        var types=["与触发量相同", "全部发送"];
        return function(input){
            return input===0||input===1?types[input]:input;
        };
    })
    .filter("timeToTrigger", function(){
        var durations=[
            0, 40, 64, 80, 100, 128, 160, 256, 320, 480, 512, 640, 1024, 1280, 2560, 5120
        ];
        return function(input){
            return angular.isNumber(input) && input>=0 && input<16 ? durations[input] : input
        }
    });