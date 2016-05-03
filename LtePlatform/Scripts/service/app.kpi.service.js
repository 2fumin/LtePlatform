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
            calculatePreciseRating: function(precise) {
                if (precise > 94.6) return 5;
                else if (precise > 93.6) return 4;
                else if (precise > 92.6) return 3;
                else if (precise > 91.6) return 2;
                else if (precise > 90) return 1;
                else return 0;
            },
            calculateDropStar: function (drop) {
                if (drop < 0.2) return 5;
                else if (drop < 0.3) return 4;
                else if (drop < 0.35) return 3;
                else if (drop < 0.4) return 2;
                else if (drop < 0.5) return 1;
                else return 0;
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
            getMrsDistrictOptions: function(stats, districts){
                var chart = new ComboChart();
                chart.title.text = "MR总数变化趋势图";
                var statDates = [];
                var districtStats = [];
                for (var i = 0; i < stats.length; i++) {
                    var stat = stats[i];
                    statDates.push(stat.statDate);
                    for (var j = 0; j < districts.length ; j++) {
                        if (i === 0) {
                            districtStats.push([stat.values[j].mr]);
                        } else {
                            districtStats[j].push(stat.values[j].mr);
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
            getPreciseDistrictOptions: function(stats, districts){
                var chart = new ComboChart();
                chart.title.text = "精确覆盖率变化趋势图";
                var statDates = [];
                var districtStats = [];
                for (var i = 0; i < stats.length; i++) {
                    var stat = stats[i];
                    statDates.push(stat.statDate);
                    for (var j = 0; j < districts.length ; j++) {
                        if (i === 0) {
                            districtStats.push([stat.values[j].precise]);
                        } else {
                            districtStats[j].push(stat.values[j].precise);
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
            generateDistrictStats: function (districts, stats) {
                var outputStats = [];
                angular.forEach(stats, function(stat) {
                    var districtViews = stat.districtPreciseViews;
                    var statDate = stat.statDate;
                    var totalMrs = 0;
                    var totalSecondNeighbors = 0;
                    var values = [];
                    angular.forEach(districts, function(district) {
                        for (var k = 0; k < districtViews.length; k++) {
                            var view = districtViews[k];
                            if (view.district === district) {
                                values.push({
                                    mr: view.totalMrs,
                                    precise: view.preciseRate
                                });
                                totalMrs += view.totalMrs;
                                totalSecondNeighbors += view.secondNeighbors;
                                break;
                            }
                        }
                        if (k === districtViews.length) {
                            values.push({
                                mr: 0,
                                precise: 0
                            });
                        }

                    });
                    values.push({
                        mr: totalMrs,
                        precise: 100 - 100 * totalSecondNeighbors / totalMrs
                    });
                    outputStats.push({
                        statDate: statDate,
                        values: values
                    });
                });
                return outputStats;
            },
            calculateAverageRates: function(stats) {
                var result = {
                    statDate: "平均值",
                    values: []
                };
                if (stats.length === 0) return result;
                for (var i = 0; i < stats.length; i++) {
                    for (var j = 0; j < stats[i].values.length; j++) {
                        if (i === 0) {
                            result.values.push({
                                mr: stats[i].values[j].mr / stats.length,
                                precise: stats[i].values[j].precise / stats.length
                            });
                        } else {
                            result.values[j].mr += stats[i].values[j].mr / stats.length;
                            result.values[j].precise += stats[i].values[j].precise / stats.length;
                        }
                    }
                }
                return result;
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
                angular.forEach(trendStat.districtStats, function(stat) {
                    calculateDistrictRates(stat);
                });
                angular.forEach(trendStat.townStats, function(stat) {
                    calculateTownRates(stat);
                });
            },
            getPreciseObject: function(district) {
                var objectTable = {
                    "禅城": 94,
                    "南海": 94,
                    "三水": 94,
                    "高明": 94,
                    "顺德": 90
                };
                return objectTable[district] === undefined ? 94 : objectTable[district];
            }
        }
    })
    .factory('topPreciseService', function ($q, $http, appUrlService) {
        return {
            getOrderSelection: function() {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('PreciseStat'),
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryTopKpis: function(begin, end, topCount, orderSelection) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('PreciseStat'),
                        params: {
                            'begin': begin,
                            'end': end,
                            'topCount': topCount,
                            'orderSelection': orderSelection
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryTopKpisInDistrict: function(begin, end, topCount, orderSelection, city, district) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('PreciseStat'),
                        params: {
                            'begin': begin,
                            'end': end,
                            'topCount': topCount,
                            'orderSelection': orderSelection,
                            city: city,
                            district: district
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            addMonitor: function(cell) {
                $http.post(appUrlService.getApiUrl('NeighborMonitor'), {
                    cellId: cell.cellId,
                    sectorId: cell.sectorId
                }).success(function() {
                    cell.isMonitored = true;
                });
            },
            queryMonitor: function(cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('NeighborMonitor'),
                        params: {
                            'cellId': cellId,
                            'sectorId': sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            updateInterferenceNeighbor: function(cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('InterferenceNeighbor'),
                        params: {
                            'cellId': cellId,
                            'sectorId': sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryInterferenceNeighbor: function(begin, end, cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('InterferenceNeighbor'),
                        params: {
                            'begin': begin,
                            'end': end,
                            'cellId': cellId,
                            'sectorId': sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            updateInterferenceVictim: function(cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('InterferenceNeighbor'),
                        params: {
                            neighborCellId: cellId,
                            neighborSectorId: sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryInterferenceVictim: function(begin, end, cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                        method: 'GET',
                        url: appUrlService.getApiUrl('InterferenceVictim'),
                        params: {
                            'begin': begin,
                            'end': end,
                            'cellId': cellId,
                            'sectorId': sectorId
                        }
                    }).success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            queryCellStastic: function(cellId, pci, begin, end){
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CellStastic'),
                    params: {
                        eNodebId: cellId,
                        pci: pci,
                        begin: begin,
                        end: end
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                    .error(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            getInterferencePieOptions: function(interferenceCells, currentCellName) {
                var over6DbPie = new GradientPie();
                var over10DbPie = new GradientPie();
                var mod3Pie = new GradientPie();
                var mod6Pie = new GradientPie();
                over6DbPie.series[0].name = '6dB干扰日平均次数';
                over10DbPie.series[0].name = '10dB干扰日平均次数';
                over6DbPie.title.text = currentCellName + ': 6dB干扰日平均次数';
                over10DbPie.title.text = currentCellName + ': 10dB干扰日平均次数';
                mod3Pie.series[0].name = 'MOD3干扰日平均次数';
                mod6Pie.series[0].name = 'MOD6干扰日平均次数';
                mod3Pie.title.text = currentCellName + ': MOD3干扰日平均次数';
                mod6Pie.title.text = currentCellName + ': MOD6干扰日平均次数';
                angular.forEach(interferenceCells, function(cell) {
                    over6DbPie.series[0].data.push({
                        name: cell.neighborCellName,
                        y: cell.overInterferences6Db
                    });
                    over10DbPie.series[0].data.push({
                        name: cell.neighborCellName,
                        y: cell.overInterferences10Db
                    });
                    if (cell.mod3Interferences > 0) {
                        mod3Pie.series[0].data.push({
                            name: cell.neighborCellName,
                            y: cell.mod3Interferences
                        });
                    }
                    if (cell.mod6Interferences > 0) {
                        mod6Pie.series[0].data.push({
                            name: cell.neighborCellName,
                            y: cell.mod6Interferences
                        });
                    }
                });
                return {
                    over6DbOption: over6DbPie.options,
                    over10DbOption: over10DbPie.options,
                    mod3Option: mod3Pie.options,
                    mod6Option: mod6Pie.options
                };
            },
            getStrengthColumnOptions: function (interferenceCells, mrCount, currentCellName) {
                var over6DbColumn = new Column3d();
                var over10DbColumn = new Column3d();
                over6DbColumn.series[0].name = '6dB干扰强度';
                over10DbColumn.series[0].name = '10dB干扰强度';
                over6DbColumn.title.text = currentCellName + ': 6dB干扰干扰强度';
                over10DbColumn.title.text = currentCellName + ': 10dB干扰干扰强度';
                
                angular.forEach(interferenceCells, function(cell) {
                    over6DbColumn.series[0].data.push(cell.overInterferences6Db / mrCount * 100);
                    over10DbColumn.series[0].data.push(cell.overInterferences10Db / mrCount * 100);
                    over6DbColumn.xAxis.categories.push(cell.neighborCellName);
                    over10DbColumn.xAxis.categories.push(cell.neighborCellName);
                });
                return {
                    over6DbOption: over6DbColumn.options,
                    over10DbOption: over10DbColumn.options
                };
            }
        };
    })
    .factory('cellPreciseService', function($q, $http, appUrlService) {
        return{
            queryDataSpanKpi: function (begin, end, cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseStat'),
                    params: {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryOneWeekKpi: function (cellId, sectorId) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('PreciseStat'),
                    params: {
                        'cellId': cellId,
                        'sectorId': sectorId
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            getMrsOptions: function (stats, title) {
                var chart = new ComboChart();
                chart.title.text = title;
                var statDates = [];
                var mrStats = [];
                var firstNeighbors = [];
                var secondNeighbors = [];
                var thirdNeighbors = [];
                for (var i = 0; i < stats.length; i++) {
                    var stat = stats[i];
                    statDates.push(stat.dateString);
                    mrStats.push(stat.totalMrs);
                    firstNeighbors.push(stat.firstNeighbors);
                    secondNeighbors.push(stat.secondNeighbors);
                    thirdNeighbors.push(stat.thirdNeighbors);
                }
                chart.xAxis[0].categories = statDates;
                chart.yAxis[0].title.text = "MR数量";
                chart.xAxis[0].title.text = '日期';
                chart.series.push({
                    type: "column",
                    name: "MR总数",
                    data: mrStats
                });
                chart.series.push({
                    type: "spline",
                    name: "第一邻区MR数",
                    data: firstNeighbors
                });
                chart.series.push({
                    type: "spline",
                    name: "第二邻区MR数",
                    data: secondNeighbors
                });
                chart.series.push({
                    type: "spline",
                    name: "第三邻区MR数",
                    data: thirdNeighbors
                });
                return chart.options;
            },
            getPreciseOptions: function (stats, title) {
                var chart = new ComboChart();
                chart.title.text = title;
                var statDates = [];
                var firstRate = [];
                var secondRate = [];
                var thirdRate = [];
                for (var i = 0; i < stats.length; i++) {
                    var stat = stats[i];
                    statDates.push(stat.dateString);
                    firstRate.push(100 - parseFloat(stat.firstRate));
                    secondRate.push(100 - parseFloat(stat.secondRate));
                    thirdRate.push(100 - parseFloat(stat.thirdRate));
                }
                chart.xAxis[0].categories = statDates;
                chart.xAxis[0].title.text = '日期';
                chart.yAxis[0].title.text = "精确覆盖率";
                chart.series.push({
                    type: "spline",
                    name: "第一邻区精确覆盖率",
                    data: firstRate
                });
                chart.series.push({
                    type: "spline",
                    name: "第二邻区精确覆盖率",
                    data: secondRate
                });
                chart.series.push({
                    type: "spline",
                    name: "第三邻区精确覆盖率",
                    data: thirdRate
                });
                return chart.options;
            }
        };
    });