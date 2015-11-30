function MapViewModel(app, dataModel) {
    var self = this;

    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    Sammy(function () {
        this.get('#collegeMap', function () {
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
        });
        this.get('/College/Map', function () { this.app.runRoute('get', '#collegeMap'); });
    });

    self.refresh = function () { };

    return self;
}

app.addViewModel({
    name: "Map",
    bindingMemberName: "collegeMap",
    factory: MapViewModel
});