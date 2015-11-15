var queryCells = function () {
    sendRequest(app.dataModel.collegePreciseUrl, "GET", {
        collegeName: app.selectedCollege(),
        begin: app.startDate(),
        end: app.endDate()
    }, function (data) {
        app.cellList(data);
    });
};

var queryPrecise = function (cell) {
    var chart = new comboChart();
    chart.title.text = cell.eNodebName + "-" + cell.sectorId + '精确覆盖率变化趋势';
    var data = {
        cellId: cell.eNodebId,
        sectorId: cell.sectorId,
        begin: app.startDate(),
        end: app.endDate()
    };
    var dom = {
        tag: "#dialog-modal",
        width: 900,
        height: 480
    };
    var dates = [];
    var firstRates = [];
    var secondRates = [];
    var thirdRates = [];
    var mrs = [];
    $.ajax({
        url: app.dataModel.preciseStatUrl,
        type: "GET",
        dataType: "json",
        data: data,
        success: function (result) {
            $(result).each(function (index) {
                dates.push(result[index].dateString);
                mrs.push(result[index].totalMrs);
                firstRates.push(result[index].firstRate);
                secondRates.push(result[index].secondRate);
                thirdRates.push(result[index].thirdRate);
            });

            chart.xAxis[0].categories = dates;
            chart.xAxis[0].title.text = "日期";

            chart.yAxis[0].title.text = 'MR数量';
            chart.yAxis[0].labels.format = '{value} 次';
            chart.yAxis.push({
                title: {
                    text: '重叠覆盖率',
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
                name: 'MR数量',
                data: mrs
            });
            chart.series.push({
                name: '第一邻区重叠覆盖率(%)',
                type: 'spline',
                yAxis: 1,
                data: firstRates,
                tooltip: {
                    valueSuffix: '%'
                }
            });
            chart.series.push({
                name: '第二邻区重叠覆盖率(%)',
                type: 'spline',
                yAxis: 1,
                data: secondRates,
                tooltip: {
                    valueSuffix: '%'
                }
            });
            chart.series.push({
                name: '第三邻区重叠覆盖率(%)',
                type: 'spline',
                yAxis: 1,
                data: thirdRates,
                tooltip: {
                    valueSuffix: '%'
                }
            });

            $(dom.tag).dialog({
                modal: true,
                title: chart.title.text,
                hide: 'slide',
                width: dom.width,
                height: dom.height,
                buttons: {
                    '关闭': function () {
                        $(dom.tag).dialog("close");
                    }
                },
                open: function () {
                    $(dom.tag).html("");
                    $(dom.tag).highcharts(chart.options);
                }
            });
        }
    });
};