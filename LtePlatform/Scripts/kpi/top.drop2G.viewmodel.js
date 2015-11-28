function TopDrop2GViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.oneDayCells = ko.observableArray([]);

    app.initialize = function () {
        $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        initializeCityKpi();
    };

    app.showKpi = function () {
        sendRequest(app.dataModel.topDrop2GUrl, "GET", {
            statDate: app.statDate(),
            city: app.currentCity()
        }, function (data) {
            app.oneDayCells(data);
        });
    }

    app.trendTable = function () {

    };

    app.trendChart = function () {

    };

    return self;
}

app.addViewModel({
    name: "TopDrop2G",
    bindingMemberName: "topDrop2G",
    factory: TopDrop2GViewModel
});