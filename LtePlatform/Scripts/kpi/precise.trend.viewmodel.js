function PreciseTrendViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable();
    self.cities = ko.observableArray([]);
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.districts = ko.observableArray([]);
    self.mrStats = ko.observableArray([]);
    self.preciseStats = ko.observableArray([]);

    Sammy(function () {
        this.get('#preciseTrend', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCityKpi(self);
        });
        this.get('/Kpi/PreciseTrend', function () { this.app.runRoute('get', '#preciseTrend'); });
    });

    self.showKpi = function () {
        sendRequest(app.dataModel.cityListUrl, "GET", {
            city: self.currentCity()
        }, function(result) {
            result.push(self.currentCity());
            self.districts(result);
        });
    };

    self.showTrend = function () {
        self.mrStats([]);
        sendRequest(app.dataModel.preciseRegionUrl, "GET", {
            begin: self.beginDate,
            end: self.endDate,
            city: self.currentCity
        }, function (result) {
            for (var i = 0; i < result.length; i++) {
                var districtViews = result[i].districtPreciseViews;
                var statDate = result[i].statDate;
                var totalMrs = 0;
                var districtMrStats = [];
                for (var j = 0; j < self.districts().length - 1; j++) {
                    var currentDistrictMrs = 0;
                    for (var k = 0; k < districtViews.length; k++) {
                        var view = districtViews[k];
                        if (view.district === self.districts()[j]) {
                            currentDistrictMrs = view.totalMrs;
                            totalMrs += currentDistrictMrs;
                            break;
                        }
                    }
                    districtMrStats.push(currentDistrictMrs);
                }
                districtMrStats.push(totalMrs);
                self.mrStats.push({
                    statDate: statDate,
                    values: districtMrStats
                });
            }
        });
    };

    return self;
}

app.addViewModel({
    name: "PreciseTrend",
    bindingMemberName: "preciseTrend",
    factory: PreciseTrendViewModel
});