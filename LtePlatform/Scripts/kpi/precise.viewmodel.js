function PreciseViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    app.currentDistrict = ko.observable("");
    app.districtStats = ko.observableArray([]);
    app.townStats = ko.observableArray([]);

    app.initialize = function () {
        $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });

        initializeCityKpi();
    };

    app.showKpi = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.preciseRegionUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: app.currentCity(),
                statDate: app.statDate()
            },
            success: function (data) {
                app.statDate(data.statDate);
                app.districtStats(data.districtPreciseViews);
                app.townStats(data.townPreciseViews);
                app.currentDistrict(data.districtPreciseViews[0].district);
                showMrPie(app.districtStats(), app.townStats());
            }
        });
    };

    app.showDetails = function(district) {
        app.currentDistrict(district);
    };

    return self;
}

app.addViewModel({
    name: "Precise",
    bindingMemberName: "precise",
    factory: PreciseViewModel
});