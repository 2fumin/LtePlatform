angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function ($q, $http, appUrlService) {
        return {
            initializeCities: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).success(function (result) {
                    deferred.resolve({
                        options: result,
                        selected: result[0]
                    });
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            }
        };
    })
    .factory('appFormatService', function() {
        return {
            getDate: function (strDate) {
                var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/,
                    function (a) { return parseInt(a, 10) - 1; }).match(/\d+/g) + ')');
                return date;
            },
            getDateString: function(dateTime, fmt) {
                var o = {
                    "M+": dateTime.getMonth() + 1, //月份 
                    "d+": dateTime.getDate(), //日 
                    "h+": dateTime.getHours(), //小时 
                    "m+": dateTime.getMinutes(), //分 
                    "s+": dateTime.getSeconds(), //秒 
                    "q+": Math.floor((dateTime.getMonth() + 3) / 3), //季度 
                    "S": dateTime.getMilliseconds() //毫秒 
                };
                if (/(y+)/.test(fmt))
                    fmt = fmt.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
                for (var k in o)
                    if (o.hasOwnProperty(k))
                        if (new RegExp("(" + k + ")").test(fmt))
                            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                return fmt;
            }
        }
    });