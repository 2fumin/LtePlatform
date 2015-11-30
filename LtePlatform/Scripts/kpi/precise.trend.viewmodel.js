function PreciseTrendViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable();
    self.cities = ko.observableArray([]);
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.districts = ko.observableArray([]);
    self.mrStats = ko.observableArray([]);

    Sammy(function () {
        this.get('#preciseTrend', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCityKpi(self);
        });
        this.get('/Kpi/PreciseTrend', function () { this.app.runRoute('get', '#preciseTrend'); });
    });

    self.showKpi = function () {
        self.districts(['A', 'B']);
    };

    self.showTrend = function () {
        self.mrStats([
            { statDate: '2015-1-1', values: [1, 2] },
            { statDate: '2015-1-2', values: [3, 4] }
        ]);
    };

    return self;
}

app.addViewModel({
    name: "PreciseTrend",
    bindingMemberName: "preciseTrend",
    factory: PreciseTrendViewModel
});