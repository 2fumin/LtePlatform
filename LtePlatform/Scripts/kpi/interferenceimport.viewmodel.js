function InterferenceImportViewModel(app, dataModel) {
    var self = this;

    self.totalDumpItems = ko.observable(0);
    self.totalSuccessItems = ko.observable(0);
    self.totalFailItems = ko.observable(0);

    Sammy(function () {
        this.get('#interferenceImport', function () {
        });
        this.post('#interferencePost', function () {
            sendRequest(app.dataModel.dumpNeighborUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });
        this.get('/Kpi/InterferenceImport', function () { this.app.runRoute('get', '#interferenceImport'); });
        this.get('/Kpi/InterferencePost', function () { this.app.runRoute('post', '#interferencePost'); });
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
    name: "InterferenceImport",
    bindingMemberName: "interferenceImport",
    factory: InterferenceImportViewModel
});