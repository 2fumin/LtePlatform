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
            generateDistrictStats(self, result);

            if (result.length > 0) {
                var districtStats = result[0].districtPreciseViews;
                var townStats = result[0].townPreciseViews;
                for (var i = 1; i < result.length; i++) {
                    for (var j = 0; j < result[i].districtPreciseViews.length; j++) {
                        var currentDistrictStat = result[i].districtPreciseViews[j];
                        for (var k = 0; k < districtStats.length; k++) {
                            if (districtStats[k].city === currentDistrictStat.city
                                && districtStats[k].district === currentDistrictStat.district) {
                                accumulatePreciseStat(districtStats[k], currentDistrictStat);
                                break;
                            }
                        }
                        if (k === districtStats.length) {
                            districtStats.push(currentDistrictStat);
                        }
                    }
                    for (j = 0; j < result[i].townPreciseViews.length; j++) {
                        var currentTownStat = result[i].townPreciseViews[j];
                        for (k = 0; k < townStats.length; k++) {
                            if (townStats[k].city===currentTownStat.city
                                &&townStats[k].district===currentTownStat.district
                                && townStats[k].town === currentTownStat.town) {
                                accumulatePreciseStat(townStats[k], currentTownStat);
                                break;
                            }
                        }
                        if (k === townStats.length) {
                            townStats.push(currentTownStat);
                        }
                    }
                }
                for (k = 0; k < districtStats.length; k++) {
                    calculateDistrictRates(districtStats[k]);
                }
                for (k = 0; k < townStats.length; k++) {
                    calculateTownRates(townStats[k]);
                }
                showMrPie(districtStats, townStats);
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