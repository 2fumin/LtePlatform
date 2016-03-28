function InfrastructureViewModel(app, dataModel) {
    var self = this;

    self.showCells = function (name) {
        sendRequest(app.dataModel.collegeCellsUrl, "GET", { collegeName: name }, function (data) {
            self.cellList(data);
            self.setView('cells');
            self.currentName(name);
        });
    };

    self.showBtss = function (name) {
        sendRequest(app.dataModel.collegeBtssUrl, "GET", { collegeName: name }, function (data) {
            self.btsList(data);
            self.setView('btss');
            self.currentName(name);
        });
    };

    self.showCdmaCells = function (name) {
        sendRequest(app.dataModel.collegeCdmaCellsUrl, "GET", { collegeName: name }, function (data) {
            self.cdmaCellList(data);
            self.setView('cdmaCells');
            self.currentName(name);
        });
    };

    self.showLteDistributions = function (name) {
        sendRequest(app.dataModel.collegeLteDistributionsUrl, "GET", { collegeName: name }, function (data) {
            self.distributionList(data);
            self.setView('lteDistributions');
            self.currentName(name);
        });
    };

    self.showCdmaDistributions = function (name) {
        sendRequest(app.dataModel.collegeCdmaDistributionsUrl, "GET", { collegeName: name }, function (data) {
            self.distributionList(data);
            self.setView('cdmaDistributions');
            self.currentName(name);
        });
    };

    self.loadAlarms = function(id) {
        return app.dataModel.loadAlarms(id, self);
    };
    
    app.view(self);
    return self;
}

app.addViewModel({
    name: "Infrastructure",
    bindingMemberName: "infrastructure",
    factory: InfrastructureViewModel
});