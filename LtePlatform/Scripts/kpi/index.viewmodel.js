function IndexViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.view = ko.observable('主要');
    app.viewOptions = ko.observableArray(['主要', '2G', '3G']);
    app.kpiDateList = ko.observableArray([]);
    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.kpiSelection = ko.observable('掉话率');
    app.kpiOptions = ko.observableArray([
        '2G全天话务量',
        '掉话率',
        '2G呼建',
        'Ec/Io优良率',
        '2G利用率',
        '全天流量(GB)',
        '3G全天话务量',
        '掉线率',
        '3G连接',
        'C/I优良率',
        '反向链路繁忙率',
        '3G切2G流量比',
        '3G利用率'
    ]);

    app.initialize = function () {
        $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        // Make a call to the protected Web API by passing in a Bearer Authorization Header
        $.ajax({
            method: 'get',
            url: app.dataModel.cityListUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function(data) {
                app.cities(data);
                if (data.length > 0) {
                    app.currentCity(data[0]);
                    app.showKpi();
                }
            }
        });
    };

    app.showKpi = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.kpiDataListUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: app.currentCity(),
                statDate: app.statDate()
            },
            success: function (data) {
                app.statDate(data.statDate);
                app.kpiDateList(data.statViews);
            }
        });
    };

    app.showTrend = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.kpiDataListUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: app.currentCity(),
                beginDate: app.beginDate(),
                endDate: app.endDate()
            },
            success: function (data) {
                $(".kpi-trend").each(function () {
                    var chart = new comboChart();
                    chart.title.text = $(this).attr('name');
                    chart.categories = data.statDates;
                    chart.yAxis.title.text = chart.title.text;
                    chart.xAxis.title.text = '日期';
                    for (var i = 0; i < data.regionList.length - 1; i++) {
                        chart.series.push({
                            type: 'column',
                            name: data.regionList[i],
                            data: data.kpiDetails[i][chart.title.text]
                        });
                    }
                    chart.series.push({
                        type: 'spline',
                        name: app.currentCity(),
                        data: data.kpiDetails[data.regionList.length - 1][chart.title.text],
                        marker: {
                            lineWidth: 2,
                            lineColor: Highcharts.getOptions().colors[3],
                            fillColor: 'white'
                        }
                    });
                    $(this).highcharts(chart.options);
                });
            }
        });        
    };

    return self;
}

app.addViewModel({
    name: "Index",
    bindingMemberName: "index",
    factory: IndexViewModel
});