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

        return {
            getRecentPreciseRegionKpi: function(city, initialDate) {
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
            getMrPieOptions: function(districtStats, townStats) {
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
            getPreciseRateOptions: function(districtStats, townStats) {
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
            }
        }
    });