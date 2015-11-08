function MapViewModel(app, dataModel) {
    var self = this;

    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    app.initialize = function () {
        $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        initializeMap('all-map', 11);
        $.ajax({
            method: 'get',
            url: app.dataModel.collegeQueryUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                drawCollegeMap(data);
            }
        });
    };

    app.refresh = function () { };
}

app.addViewModel({
    name: "Map",
    bindingMemberName: "map",
    factory: MapViewModel
});