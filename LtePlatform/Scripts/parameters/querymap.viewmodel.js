function QueryMapViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable("");
    self.cities = ko.observableArray([]);
    self.currentDistrict = ko.observable("");
    self.districts = ko.observableArray([]);
    self.currentTown = ko.observable("");
    self.towns = ko.observableArray([]);
    self.queryText = ko.observable("");
    self.currentNetowrk = ko.observable("LTE");
    self.networks = ko.observableArray(["LTE", "CDMA"]);

    Sammy(function () {
        this.get('#queryMap', function() {

            initializeMap('mapContent', 11);

            initializeCities(self);

            self.currentCity.subscribe(function(name) {
                updateDistricts(self, name);
            });

            self.currentDistrict.subscribe(function(name) {
                updateTowns(self, name);
            });

        });
        this.get('/Parameters/QueryMap', function () { this.app.runRoute('get', '#queryMap'); });
    });

    self.queryItems = function () {
        if (self.currentNetowrk() === "LTE")
            queryENodebs(self);
        else
            queryBtss(self);
    };

    return self;
}

app.addViewModel({
    name: "QueryMap",
    bindingMemberName: "queryMap",
    factory: QueryMapViewModel
});