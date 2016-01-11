function NeighborImportViewModel(app, dataModel) {
    var self = this;

    self.initialize = ko.observable(true);
    self.totalDumpItems = ko.observable(0);
    self.totalSuccessItems = ko.observable(0);
    self.totalFailItems = ko.observable(0);
    
    Sammy(function () {
        this.get('#neighborImport', function () {
            self.initialize(true);
        });
        this.post('#neighborZtePost', function () {
            self.initialize(false);
            sendRequest(app.dataModel.dumpNeighborUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });
        this.post('#neighborHwPost', function () {
            self.initialize(false);
            sendRequest(app.dataModel.dumpNeighborUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });

        this.get('/Parameters/NeighborImport', function () { this.app.runRoute('get', '#neighborImport'); });
        this.get('/Parameters/ZteNeighborPost', function () { this.app.runRoute('post', '#neighborZtePost'); });
        this.get('/Parameters/HwNeighborPost', function () { this.app.runRoute('post', '#neighborHwPost'); });
    });
    
    self.dumpItems = function () {
        dumpProgressItems(self, app.dataModel.dumpNeighborUrl);
    };

    self.updateHistoryItems = function () {
        self.clearItems();
    };

    self.clearItems = function () {
        sendRequest(app.dataModel.dumpNeighborUrl, "DELETE", null, function () {
            self.totalDumpItems(0);
            self.totalFailItems(0);
            self.totalSuccessItems(0);
        });
    };

    return self;
}

app.addViewModel({
    name: "NeighborImport",
    bindingMemberName: "neighborImport",
    factory: NeighborImportViewModel
});