function PreciseViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable();
    self.cities = ko.observableArray([]);
    self.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));

    self.currentDistrict = ko.observable("");
    self.districtStats = ko.observableArray([]);
    self.townStats = ko.observableArray([]);

    Sammy(function () {
        this.get('#precise', function () {
            $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCityKpi(self);
        });
        this.get('/Kpi/Precise', function () { this.app.runRoute('get', '#precise'); });
    });

    self.showKpi = function () {
        $.ajax({
            method: 'get',
            url: app.dataModel.preciseRegionUrl,
            contentType: "application/json; charset=utf-8",
            data: {
                city: self.currentCity(),
                statDate: self.statDate()
            },
            success: function (data) {
                self.statDate(data.statDate);
                self.districtStats(data.districtPreciseViews);
                self.townStats(data.townPreciseViews);
                self.currentDistrict(data.districtPreciseViews[0].district);
                showMrPie(self.districtStats(), self.townStats());
            }
        });
    };

    self.showDetails = function(district) {
        self.currentDistrict(district);
    };

    return self;
}

app.addViewModel({
    name: "Precise",
    bindingMemberName: "precise",
    factory: PreciseViewModel
});