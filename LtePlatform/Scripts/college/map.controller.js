
var drawCollegeRegions = function (viewModel, result) {
    var type = result.regionType;
    var info = result.info;
    var coors = info.split(';');
    var centerx = 0;
    var centery = 0;
    if (type === 2) {
        var points = [];
        for (var p = 0; p < coors.length / 2; p++) {
            points.push(new BMap.Point(parseFloat(coors[2 * p]), parseFloat(coors[2 * p + 1])));
            centerx += parseFloat(coors[2 * p]);
            centery += parseFloat(coors[2 * p + 1]);
        }
        centerx /= coors.length / 2;
        centery /= coors.length / 2;
        var polygon = new BMap.Polygon(points,
            { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(polygon);
    } else if (type === 1) {
        centerx = (parseFloat(coors[0]) + parseFloat(coors[2])) / 2;
        centery = (parseFloat(coors[1]) + parseFloat(coors[3])) / 2;
        var rectangle = new BMap.Polygon([
            new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
            new BMap.Point(parseFloat(coors[2]), parseFloat(coors[1])),
            new BMap.Point(parseFloat(coors[2]), parseFloat(coors[3])),
            new BMap.Point(parseFloat(coors[0]), parseFloat(coors[3]))
        ], { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(rectangle);
    } else if (type === 0) {
        centerx = parseFloat(coors[0]);
        centery = parseFloat(coors[1]);
        var circle = new BMap.Circle(new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
            parseFloat(coors[2]),
            { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(circle);
    }

    for (var i = 0; i < viewModel.collegeInfos().length; i++) {
        info = viewModel.collegeInfos()[i];
        if (info.id === result.areaId) {
            info.centerx = centerx;
            info.centery = centery;
            var opts = {
                position: new BMap.Point(centerx, centery),    // 指定文本标注所在的地理位置
                offset: new BMap.Size(10, -20)    //设置文本偏移量
            };
            var label = new BMap.Label(info.name, opts);  // 创建文本标注对象
            label.setStyle({
                color: "red",
                fontSize: "12px",
                height: "20px",
                lineHeight: "20px",
                fontFamily: "微软雅黑"
            });
            map.addOverlay(label);
            break;
        }
    }

};

var drawCollegeMap = function (viewModel, data) {
    for (var index = 0; index < data.length; index++) {
        var info = {};
        info.id = data[index].id;
        info.name = data[index].name;
        viewModel.collegeInfos.push(info);
        $.ajax({
            method: 'get',
            url: app.dataModel.collegeRegionUrl + '/' + info.id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (result) {
                drawCollegeRegions(viewModel, result);
            }
        });
        $.ajax({
            method: 'get',
            url: app.dataModel.collegeStatUrl + '/' + info.id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (college) {
                addOneCollegeMarkerInfo(viewModel, college);
            }
        });
    }
};

var drawCollegeENodebs = function(viewModel) {
    if (map.eNodebMarkers.length > 0) return;
    sendRequest(app.dataModel.collegeENodebUrl, "POST", {
        names: viewModel.collegeNames()
    }, function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneENodebMarker(result[i]);
        }
    });
};

var drawCollegeBtss = function(viewModel) {
    if (map.btsMarkers.length > 0) return;
    sendRequest(app.dataModel.collegeBtssUrl, "POST", {
        names: viewModel.collegeNames()
    }, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneBtsMarker(result[i]);
        }
    });
};

var drawCollegeCells = function(viewModel) {
    if (map.lteSectors.length > 0) return;
    sendRequest(app.dataModel.collegeCellsUrl, "POST", {
        names: viewModel.collegeNames()
    }, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneGeneralSector(result[i], "LteCell");
        }
    });
};

var drawCollegeCdmaCells = function(viewModel) {
    if (map.cdmaSectors.length > 0) return;
    sendRequest(app.dataModel.collegeCdmaCellsUrl, "POST", {
        names: viewModel.collegeNames()
    }, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneGeneralSector(result[i], "CdmaCell");
        }
    });
};

var drawCollegeLteDistributions = function(viewModel) {
    if (map.lteDistributions.length > 0) return;
    sendRequest(app.dataModel.collegeLteDistributionsUrl, "POST", {
        names: viewModel.collegeNames()
    }, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneLteDistributionMarkerInfo(result[i]);
        }
    });
};

var drawCollegeCdmaDistributions = function(viewModel) {
    if (map.cdmaDistributions.length > 0) return;
    sendRequest(app.dataModel.collegeCdmaDistributionsUrl, "POST", {
        names: viewModel.collegeNames()
    }, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneCdmaDistributionMarkerInfo(result[i]);
        }
    });
};

var matchCollegeStats = function(names, dict) {
    var stats = [];
    for (var i = 0; i < names.length; i++) {
        if (dict[names[i]] !== undefined) {
            stats.push(dict[names[i]]);
        } else {
            stats.push(null);
        }
    }
    return stats;
};

var showCollegeRates = function(collegeNames, downloadRates, uploadRates, evdoRates, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网速率统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '数据速率';
    chart.yAxis[0].labels.format = '{value} kByte/s';

    chart.series.push({
        type: 'column',
        name: '4G下行速率',
        data: downloadRates
    });
    chart.series.push({
        type: 'column',
        name: '4G上行速率',
        data: uploadRates
    });
    chart.series.push({
        type: 'line',
        name: '3G下行速率',
        data: evdoRates
    });

    showChartDialog(tag, chart);
};

var showCollegeUsers = function(collegeNames, lteUsers, evdoUsers, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网平均用户数统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '平均用户数';

    chart.series.push({
        type: 'column',
        name: '4G用户数',
        data: lteUsers
    });
    chart.series.push({
        type: 'column',
        name: '3G用户数',
        data: evdoUsers
    });

    showChartDialog(tag, chart);
};

var showCollegeCoverage = function(collegeNames, rsrpStats, sinrStats, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网4G覆盖指标统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = 'RSRP';
    chart.yAxis[0].labels.format = '{value} dBm';
    chart.yAxis.push({
        title: {
            text: 'SINR',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        labels: {
            format: '{value} dB',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        opposite: true
    });

    chart.series.push({
        type: 'column',
        name: 'RSRP(dBm)',
        data: rsrpStats,
        tooltip: {
            valueSuffix: 'dBm'
        }
    });
    chart.series.push({
        type: 'line',
        name: 'SINR(dB)',
        data: sinrStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'dB'
        }
    });
    
    showChartDialog(tag, chart);
};

var showCollegeInterference = function(collegeNames, minRssiStats, maxRssiStats, vswrStats, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网3G干扰指标统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '驻波比';
    chart.yAxis.push({
        title: {
            text: 'RSSI',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        labels: {
            format: '{value} dBm',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        opposite: true
    });

    chart.series.push({
        type: 'column',
        name: '驻波比',
        data: vswrStats
    });
    chart.series.push({
        type: 'line',
        name: 'RSSI最大值',
        data: maxRssiStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'dBm'
        }
    });
    chart.series.push({
        type: 'line',
        name: 'RSSI最小值',
        data: minRssiStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'dBm'
        }
    });

    showChartDialog(tag, chart);
};

var showCollegeDrop = function (collegeNames, erlang3G, erabDrop, drop3G, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网保持性能指标统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '3G话务量';
    chart.yAxis.push({
        title: {
            text: '掉线率',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        labels: {
            format: '{value} %',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        opposite: true
    });

    chart.series.push({
        type: 'column',
        name: '3G话务量',
        data: erlang3G
    });
    chart.series.push({
        type: 'line',
        name: 'E-RAB掉线率',
        data: erabDrop,
        yAxis: 1,
        tooltip: {
            valueSuffix: '%'
        }
    });
    chart.series.push({
        type: 'line',
        name: '3G掉线率',
        data: drop3G,
        yAxis: 1,
        tooltip: {
            valueSuffix: '%'
        }
    });

    showChartDialog(tag, chart);
}

var showCollegeFlows = function(collegeNames, usersStats, downloadFlowStats, uploadFlowStats, flow3GStats, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网业务流量指标统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '用户数';
    chart.yAxis.push({
        title: {
            text: '流量',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        labels: {
            format: '{value} GB',
            style: {
                color: Highcharts.getOptions().colors[1]
            }
        },
        opposite: true
    });

    chart.series.push({
        type: 'column',
        name: '4G用户数',
        data: usersStats
    });
    chart.series.push({
        type: 'line',
        name: '4G下行流量',
        data: downloadFlowStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'GB'
        }
    });
    chart.series.push({
        type: 'line',
        name: '4G上行流量',
        data: uploadFlowStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'GB'
        }
    });
    chart.series.push({
        type: 'line',
        name: '3G流量',
        data: flow3GStats,
        yAxis: 1,
        tooltip: {
            valueSuffix: 'GB'
        }
    });

    showChartDialog(tag, chart);
};

var showCollegeConnection = function (collegeNames, rrcConnection, erabConnection, connection2G, connection3G, tag) {
    var chart = new ComboChart();
    chart.title.text = "校园网业务流量指标统计";

    chart.xAxis[0].categories = collegeNames;
    chart.xAxis[0].title.text = "校园名称";

    chart.yAxis[0].title.text = '连接成功率';

    chart.series.push({
        type: 'spline',
        name: 'RRC连接成功率',
        data: rrcConnection
    });
    chart.series.push({
        type: 'spline',
        name: 'E-RAB连接成功率',
        data: erabConnection
    });
    chart.series.push({
        type: 'line',
        name: '2G呼建成功率',
        data: connection2G
    });
    chart.series.push({
        type: 'line',
        name: '3G连接成功率',
        data: connection3G
    });

    showChartDialog(tag, chart);
};

var calculate4GCoverageRate = function (viewModel) {
    var coveragePoints = 0;
    var kpiList = viewModel.coverageKpiList();
    for (var i = 0; i < kpiList.length; i++) {
        var kpi = kpiList[i];
        if (kpi.rsrp > -105 && kpi.sinr > -3) {
            coveragePoints++;
        }
    }
    viewModel.coverageRate(100 * coveragePoints / kpiList.length);
};

var calculate3GCoverageRate = function (viewModel) {
    var coveragePoints = 0;
    var kpiList = viewModel.coverageKpiList();
    for (var i = 0; i < kpiList.length; i++) {
        var kpi = kpiList[i];
        if (kpi.rxAgc0 > -95 && kpi.rxAgc1 > -95 && kpi.sinr > -3) {
            coveragePoints++;
        }
    }
    viewModel.coverageRate(100 * coveragePoints / kpiList.length);
};

var calculate2GCoverageRate = function (viewModel) {
    var coveragePoints = 0;
    var kpiList = viewModel.coverageKpiList();
    for (var i = 0; i < kpiList.length; i++) {
        var kpi = kpiList[i];
        if (kpi.rxAgc > -95 && kpi.txPower < 0 && kpi.ecio > -12) {
            coveragePoints++;
        }
    }
    viewModel.coverageRate(100 * coveragePoints / kpiList.length);
};
