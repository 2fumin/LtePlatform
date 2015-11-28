function IndexViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable();
    self.cities = ko.observableArray([]);
    self.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.view = ko.observable('主要');
    self.viewOptions = ko.observableArray(['主要', '2G', '3G']);
    self.kpiDateList = ko.observableArray([]);
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.kpiSelection = ko.observable('掉话率');
    self.kpiOptions = ko.observableArray([]);

    Sammy(function () {
        this.get('#index', function () {
            $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCityKpi(self);

            $.ajax({
                method: 'get',
                url: app.dataModel.kpiDataListUrl,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    self.kpiOptions(data);
                }
            });
        });
        this.get('/Kpi', function () { this.app.runRoute('get', '#index'); })
    });

    self.showKpi = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.kpiDataListUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: self.currentCity(),
                statDate: self.statDate()
            },
            success: function (data) {
                self.statDate(data.statDate);
                self.kpiDateList(data.statViews);
            }
        });
    };

    self.showTrend = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.kpiDataListUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: self.currentCity(),
                beginDate: self.beginDate(),
                endDate: self.endDate()
            },
            success: function (data) {
                $(".kpi-trend").each(function () {
                    var chart = new comboChart();
                    chart.title.text = $(this).attr('name');
                    var kpiOption = lowerFirstLetter(chart.title.text);
                    chart.xAxis[0].categories = data.statDates;
                    chart.yAxis[0].title.text = chart.title.text;
                    chart.xAxis[0].title.text = '日期';
                    for (var i = 0; i < data.regionList.length - 1; i++) {
                        chart.series.push({
                            type: 'column',
                            name: data.regionList[i],
                            data: data.kpiDetails[kpiOption][i]
                        });
                    }
                    chart.series.push({
                        type: 'spline',
                        name: self.currentCity(),
                        data: data.kpiDetails[kpiOption][data.regionList.length - 1],
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