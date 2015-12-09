function QueryMapViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable("");
    self.cities = ko.observableArray([]);
    self.currentDistrict = ko.observable("");
    self.districts = ko.observableArray([]);
    self.currentTown = ko.observable("");
    self.towns = ko.observableArray([]);
    self.queryText = ko.observable("");

    Sammy(function () {
        this.get('#queryMap', function() {

            initializeMap('mapContent', 11);

            initializeCities(self);

            self.currentCity.subscribe(function() {
                updateDistricts(self);
            });

            self.currentDistrict.subscribe(function() {
                updateTowns(self);
            });

        });
        this.get('/Parameters/QueryMap', function () { this.app.runRoute('get', '#queryMap'); });
    });

    self.queryENodebs = function() {
        if (self.queryText().trim() === "") {

        } else {
            
        }
    };

    return self;
}

app.addViewModel({
    name: "QueryMap",
    bindingMemberName: "queryMap",
    factory: QueryMapViewModel
});