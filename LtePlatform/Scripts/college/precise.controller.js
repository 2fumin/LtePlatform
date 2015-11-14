var queryCells = function () {
    sendRequest("/api/CollegePrecise/", "GET", {
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
    var setting = this.setting;
    $.ajax({
        url: "/api/QueryPreciseStat/",
        type: "GET",
        dataType: "json",
        data: data,
        success: function (result) {
            $(result).each(function (index) {
                dates.push(result[index].DateString);
                mrs.push(result[index].TotalMrs);
                firstRates.push(result[index].FirstRate);
                secondRates.push(result[index].SecondRate);
                thirdRates.push(result[index].ThirdRate);
            });
            setting.categories = dates;
            setting.xLabel = "日期";
            setting.setPrimaryYAxis('次', 'MR数量');
            setting.addColumnSeries(mrs, 'MR数量', '次', 0);
            setting.addYAxis('叠重覆盖率', '%', 2);
            setting.addLineSeries(firstRates, "第一邻区重叠覆盖率(%)", '%', 1);
            setting.addLineSeries(secondRates, "第二邻区重叠覆盖率(%)", '%', 1);
            setting.addLineSeries(thirdRates, "第三邻区重叠覆盖率(%)", '%', 1);
            $(dom.tag).showChartDialog(setting.title, setting.getOptions(), dom.width, dom.height);
        }
    });
};