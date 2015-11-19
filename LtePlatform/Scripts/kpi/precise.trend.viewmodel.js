function PreciseTrendViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    initializeCityKpi();
    return self;
}

app.addViewModel({
    name: "PreciseTrend",
    bindingMemberName: "preciseTrend",
    factory: PreciseTrendViewModel
});