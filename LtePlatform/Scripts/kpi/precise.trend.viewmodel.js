function PreciseTrendViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.districts = ko.observableArray([]);
    app.mrStats = ko.observableArray([]);

    app.initialize = function () {
        $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        initializeCityKpi();
    }

    app.showKpi = function () {
        app.districts(['A', 'B']);
    };

    app.showTrend = function () {
        app.mrStats([
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