function PreciseViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    app.currentDistrict = ko.observable();
    app.districtStats = ko.observableArray([]);
    app.townStats = ko.observableArray([]);

    app.initialize = function () {
        $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });

        // Make a call to the protected Web API by passing in a Bearer Authorization Header
        $.ajax({
            method: 'get',
            url: app.dataModel.cityListUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
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

    return self;
}