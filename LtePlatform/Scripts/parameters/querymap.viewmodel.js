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

            self.currentCity.subscribe(function(name) {
                updateDistricts(self, name);
            });

            self.currentDistrict.subscribe(function(name) {
                updateTowns(self, name);
            });

        });
        this.get('/Parameters/QueryMap', function () { this.app.runRoute('get', '#queryMap'); });
    });

    self.queryENodebs = function() {
        if (self.queryText().trim() === "") {
            sendRequest(app.dataModel.eNodebUrl, "GET", {
                city: self.currentCity(),
                district: self.currentDistrict(),
                town: self.currentTown()
            }, function(result) {
                for (var i = 0; i < result.length; i++) {
                    addOneENodebMarker(result[i]);
                }
            });
        } else {
            sendRequest(app.dataModel.eNodebUrl, "GET", {
                name: self.queryText()
            }, function(result) {
                for (var i = 0; i < result.length; i++) {
                    addOneENodebMarker(result[i]);
                }
            });
        }
    };

    return self;
}

app.addViewModel({
    name: "QueryMap",
    bindingMemberName: "queryMap",
    factory: QueryMapViewModel
});