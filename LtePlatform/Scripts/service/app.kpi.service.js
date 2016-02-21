angular.module('myApp.kpi', ['myApp.url'])
    .factory('appKpiService', function ($q, $http, appUrlService) {
        var accumulatePreciseStat = function (source, accumulate) {
            source.totalMrs += accumulate.totalMrs;
            source.firstNeighbors += accumulate.firstNeighbors;
            source.secondNeighbors += accumulate.secondNeighbors;
            source.thirdNeighbors += accumulate.thirdNeighbors;
        };
        var calculateDistrictRates = function (districtStat) {
            districtStat.firstRate = 100 - 100 * districtStat.firstNeighbors / districtStat.totalMrs;
            districtStat.preciseRate = 100 - 100 * districtStat.secondNeighbors / districtStat.totalMrs;
            districtStat.thirdRate = 100 - 100 * districtStat.thirdNeighbors / districtStat.totalMrs;
            return districtStat;
        };
        var calculateTownRates = function (townStat) {
            townStat.firstRate = 100 - 100 * townStat.firstNeighbors / townStat.totalMrs;
            townStat.preciseRate = 100 - 100 * townStat.secondNeighbors / townStat.totalMrs;
            townStat.thirdRate = 100 - 100 * townStat.thirdNeighbors / townStat.totalMrs;
        };

        return {
            getRecentPreciseRegionKpi: function (city, initialDate) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseRegion'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
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
            getDateSpanPreciseRegionKpi: function (city, beginDate, endDate) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseRegion'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        city: city,
                        begin: beginDate,
                        end: endDate
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            getCityStat: function (districtStats, currentCity) {
                var stat = {
                    city: currentCity,
                    district: "-",
                    totalMrs: 0,
                    firstNeighbors: 0,
                    secondNeighbors: 0,
                    thirdNeighbors: 0,
                    firstRate: 0,
                    preciseRate: 0
                };
                for (var i = 0; i < districtStats.length; i++) {
                    accumulatePreciseStat(stat, districtStats[i]);
                }
                return calculateDistrictRates(stat);
            },
            getMrPieOptions: function (districtStats, townStats) {
                var chart = new DrilldownPie();
                chart.title.text = "分镇区测量报告数分布图";
                chart.series[0].data = [];
                chart.drilldown.series = [];
                chart.series[0].name = "区域";
                for (var i = 0; i < districtStats.length; i++) {
                    var subData = [];
                    var district = districtStats[i].district;
                    var districtMr = districtStats[i].totalMrs;
                    for (var j = 0; j < townStats.length; j++) {
                        if (townStats[j].district === district) {
                            subData.push([townStats[j].town, townStats[j].totalMrs]);
                        }
                    }
                    chart.addOneSeries(district, districtMr, subData);
                }
                return chart.options;
            },
            getPreciseRateOptions: function (districtStats, townStats) {
                var chart = new DrilldownColumn();
                chart.title.text = "分镇区精确覆盖率分布图";
                chart.series[0].data = [];
                chart.drilldown.series = [];
                chart.series[0].name = "区域";
                for (var i = 0; i < districtStats.length; i++) {
                    var subData = [];
                    var district = districtStats[i].district;
                    var districtRate = districtStats[i].preciseRate;
                    for (var j = 0; j < townStats.length; j++) {
                        if (townStats[j].district === district) {
                            subData.push([townStats[j].town, townStats[j].preciseRate]);
                        }
                    }
                    chart.addOneSeries(district, districtRate, subData);
                }
                return chart.options;
            },
            getMrsDistrictOptions: function(mrStats, districts){
                var chart = new ComboChart();
                chart.title.text = "MR总数变化趋势图";
                var statDates = [];
                var districtStats = [];
                for (var i = 0; i < mrStats.length; i++) {
                    var stat = mrStats[i];
                    statDates.push(stat.statDate);
                    for (var j = 0; j < districts.length ; j++) {
                        if (i == 0) {
                            districtStats.push([stat.values[j]]);
                        } else {
                            districtStats[j].push(stat.values[j]);
                        }
                    }
                }
                chart.xAxis[0].categories = statDates;
                chart.yAxis[0].title.text = "MR总数";
                chart.xAxis[0].title.text = '日期';
                for (j = 0; j < districts.length; j++) {
                    chart.series.push({
                        type: j === districts.length - 1 ? "spline" : "column",
                        name: districts[j],
                        data: districtStats[j]
                    });
                }
                return chart.options;
            },
            getPreciseDistrictOptions: function(preciseStats, districts){
                var chart = new ComboChart();
                chart.title.text = "精确覆盖率变化趋势图";
                var statDates = [];
                var districtStats = [];
                for (var i = 0; i < preciseStats.length; i++) {
                    var stat = preciseStats[i];
                    statDates.push(stat.statDate);
                    for (var j = 0; j < districts.length ; j++) {
                        if (i == 0) {
                            districtStats.push([stat.values[j]]);
                        } else {
                            districtStats[j].push(stat.values[j]);
                        }
                    }
                }
                chart.xAxis[0].categories = statDates;
                chart.yAxis[0].title.text = "精确覆盖率";
                chart.xAxis[0].title.text = '日期';
                for (j = 0; j < districts.length; j++) {
                    chart.series.push({
                        type: j === districts.length - 1 ? "spline" : "line",
                        name: districts[j],
                        data: districtStats[j]
                    });
                }
                return chart.options;
            },
            generateDistrictStats: function (trendStat, districts, result) {
                for (var i = 0; i < result.length; i++) {
                    var districtViews = result[i].districtPreciseViews;
                    var statDate = result[i].statDate;
                    var totalMrs = 0;
                    var totalSecondNeighbors = 0;
                    var districtMrStats = [];
                    var districtPreciseRates = [];
                    for (var j = 0; j < districts.length - 1; j++) {
                        var currentDistrictMrs = 0;
                        var currentPreciseRate = 0;
                        for (var k = 0; k < districtViews.length; k++) {
                            var view = districtViews[k];
                            if (view.district === districts[j]) {
                                currentDistrictMrs = view.totalMrs;
                                currentPreciseRate = view.preciseRate;
                                totalMrs += currentDistrictMrs;
                                totalSecondNeighbors += view.secondNeighbors;
                                break;
                            }
                        }
                        districtMrStats.push(currentDistrictMrs);
                        districtPreciseRates.push(currentPreciseRate);
                    }
                    districtMrStats.push(totalMrs);
                    districtPreciseRates.push(100 - 100 * totalSecondNeighbors / totalMrs);
                    trendStat.mrStats.push({
                        statDate: statDate,
                        values: districtMrStats
                    });
                    trendStat.preciseStats.push({
                        statDate: statDate,
                        values: districtPreciseRates
                    });
                }
            },
            generateTrendStatsForPie: function (trendStat, result) {
                trendStat.districtStats = result[0].districtPreciseViews;
                trendStat.townStats = result[0].townPreciseViews;
                for (var i = 1; i < result.length; i++) {
                    for (var j = 0; j < result[i].districtPreciseViews.length; j++) {
                        var currentDistrictStat = result[i].districtPreciseViews[j];
                        for (var k = 0; k < trendStat.districtStats.length; k++) {
                            if (trendStat.districtStats[k].city === currentDistrictStat.city
                                && trendStat.districtStats[k].district === currentDistrictStat.district) {
                                accumulatePreciseStat(trendStat.districtStats[k], currentDistrictStat);
                                break;
                            }
                        }
                        if (k === trendStat.districtStats.length) {
                            trendStat.districtStats.push(currentDistrictStat);
                        }
                    }
                    for (j = 0; j < result[i].townPreciseViews.length; j++) {
                        var currentTownStat = result[i].townPreciseViews[j];
                        for (k = 0; k < trendStat.townStats.length; k++) {
                            if (trendStat.townStats[k].city === currentTownStat.city
                                && trendStat.townStats[k].district === currentTownStat.district
                                && trendStat.townStats[k].town === currentTownStat.town) {
                                accumulatePreciseStat(trendStat.townStats[k], currentTownStat);
                                break;
                            }
                        }
                        if (k === trendStat.townStats.length) {
                            trendStat.townStats.push(currentTownStat);
                        }
                    }
                }
                for (k = 0; k < trendStat.districtStats.length; k++) {
                    calculateDistrictRates(trendStat.districtStats[k]);
                }
                for (k = 0; k < trendStat.townStats.length; k++) {
                    calculateTownRates(trendStat.townStats[k]);
                }
            }
        }
    })
    .factory('topPreciseService', function ($q, $http, appUrlService) {
        return {
            getOrderSelection: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseStat'),
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
            queryTopKpis: function (begin, end, topCount, orderSelection) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseStat'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
                    params: {
                        'begin': begin,
                        'end': end,
                        'topCount': topCount,
                        'orderSelection': orderSelection
                    }
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