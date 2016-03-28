function InfrastructureViewModel(app, dataModel) {
    var self = this;

    self.setView = ko.observable('list');
    self.currentName = ko.observable();
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.collegeList = ko.observableArray([]);
    self.eNodebList = ko.observableArray([]);
    self.cellList = ko.observableArray([]);
    self.btsList = ko.observableArray([]);
    self.cdmaCellList = ko.observableArray([]);
    self.distributionList = ko.observableArray([]);
    self.alarms = ko.observableArray([]);

    self.gobackList = function () {
        self.setView('list');
    };

    self.refreshAlarms = function () {
        self.showENodebs(self.currentName());
    };

    self.showENodebs = function (name) {
        sendRequest(app.dataModel.collegeENodebUrl, "GET", {
            collegeName: name,
            begin: self.beginDate(),
            end: self.endDate()
        }, function (data) {
            self.eNodebList(data);
            self.setView('eNodebs');
            self.currentName(name);
        });
    };

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