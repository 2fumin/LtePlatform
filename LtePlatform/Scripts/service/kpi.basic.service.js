angular.module('kpi.basic', ['myApp.url', 'myApp.url'])
    .factory('kpi2GService', function($q, $http, appUrlService, appFormatService) {
        return {
            queryDayStats: function(city, initialDate) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('KpiDataList'),
                        params: {
                            city: city,
                            statDate: initialDate
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryKpiOptions: function() {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('KpiDataList')
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryKpiTrend: function(city, begin, end) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('KpiDataList'),
                        params: {
                            city: city,
                            beginDate: begin,
                            endDate: end
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            generateComboChartOptions: function(data, name, city) {
                var chart = new ComboChart();
                chart.title.text = name;
                var kpiOption = appFormatService.lowerFirstLetter(name);
                chart.xAxis[0].categories = data.statDates;
                chart.yAxis[0].title.text = name;
                chart.xAxis[0].title.text = '日期';
                for (var i = 0; i < data.regionList.length - 1; i++) {
                    chart.series.push({
                        type: kpiOption === "2G呼建(%)" ? 'line' : 'column',
                        name: data.regionList[i],
                        data: data.kpiDetails[kpiOption][i]
                    });
                }
                chart.series.push({
                    type: 'spline',
                    name: city,
                    data: data.kpiDetails[kpiOption][data.regionList.length - 1],
                    marker: {
                        lineWidth: 2,
                        lineColor: Highcharts.getOptions().colors[3],
                        fillColor: 'white'
                    }
                });
                return chart.options;
            }
        };
    })
    .factory('drop2GService', function($q, $http, appUrlService) {
        return {
            queryDayStats: function (city, initialDate) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopDrop2G'),
                    params: {
                        city: city,
                        statDate: initialDate
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryOrderPolicy: function() {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopDrop2G')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellTrend: function(begin, end, city, policy, topCount) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopDrop2G'),
                    params: {
                        begin: begin,
                        end: end,
                        city: city,
                        policy: policy,
                        topCount: topCount
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        }
    })
    .factory('connection3GService', function ($q, $http, appUrlService) {
        return {
            queryDayStats: function (city, initialDate) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopConnection3G'),
                    params: {
                        city: city,
                        statDate: initialDate
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryOrderPolicy: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopConnection3G')
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellTrend: function (begin, end, city, policy, topCount) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('TopConnection3G'),
                    params: {
                        begin: begin,
                        end: end,
                        city: city,
                        policy: policy,
                        topCount: topCount
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            }
        }
    });