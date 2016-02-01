var showTypePieChart = function(views, tag) {
    var stats = [];
    var i;
    for (i = 0; i < views.length; i++) {
        var type = views[i].workItemType;
        var subType = views[i].workItemSubType;
        var j;
        for (j = 0; j < stats.length; j++) {
            if (stats[j].type === type) {
                stats[j].total++;
                var subData = stats[j].subData;
                var k;
                for (k = 0; k < subData.length; k++) {
                    if (subData[k][0] === subType) {
                        subData[k][1]++;
                        break;
                    }
                }
                if (k === subData.length) {
                    subData.push([subType, 1]);
                }
                break;
            }
        }
        if (j === stats.length) {
            stats.push({
                type: type,
                total: 1,
                subData: [[subType, 1]]
            });
        }
    }

    var chart = new DrilldownPie();
    chart.title.text = "工单类型分布图";
    chart.series[0].data = [];
    chart.drilldown.series = [];
    chart.series[0].name = "工单类型";
    for (i = 0; i < stats.length; i++) {
        chart.addOneSeries(stats[i].type, stats[i].total, stats[i].subData);
    }
    $(tag).highcharts(chart.options);
};

var showStatePieChart = function (views, tag) {
    var stats = [];
    var i;
    for (i = 0; i < views.length; i++) {
        var state = views[i].workItemState;
        var subType = views[i].workItemSubType;
        var j;
        for (j = 0; j < stats.length; j++) {
            if (stats[j].state === state) {
                stats[j].total++;
                var subData = stats[j].subData;
                var k;
                for (k = 0; k < subData.length; k++) {
                    if (subData[k][0] === subType) {
                        subData[k][1]++;
                        break;
                    }
                }
                if (k === subData.length) {
                    subData.push([subType, 1]);
                }
                break;
            }
        }
        if (j === stats.length) {
            stats.push({
                state: state,
                total: 1,
                subData: [[subType, 1]]
            });
        }
    }

    var chart = new DrilldownPie();
    chart.title.text = "工单状态分布图";
    chart.series[0].data = [];
    chart.drilldown.series = [];
    chart.series[0].name = "工单状态";
    for (i = 0; i < stats.length; i++) {
        chart.addOneSeries(stats[i].state, stats[i].total, stats[i].subData);
    }
    $(tag).highcharts(chart.options);
};
