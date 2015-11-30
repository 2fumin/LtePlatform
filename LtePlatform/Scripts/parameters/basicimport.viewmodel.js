function BasicImportViewModel(app, dataModel) {
    var self = this;

    self.newENodebs = ko.observableArray([]);
    self.newCells = ko.observableArray([]);
    self.newBtss = ko.observableArray([]);
    self.newCdmaCells = ko.observableArray([]);
    self.newENodebsVisible = ko.observable(false);
    self.newCellsVisible = ko.observable(false);
    self.newBtssVisible = ko.observable(false);
    self.newCdmaCellsVisible = ko.observable(false);
    self.newENodebsImport = ko.observable(true);
    self.newCellsImport = ko.observable(true);
    self.newBtssImport = ko.observable(true);
    self.newCdmaCellsImport = ko.observable(true);

    Sammy(function () {
        this.get('#basicImport', function () {
            sendRequest(app.dataModel.newENodebExcelsUrl, "GET", null, function(data) {
                self.newENodebs(data);
            });
        });
        this.get('#basicImport', function () {
            sendRequest(app.dataModel.newCellExcelsUrl, "GET", null, function (data) {
                self.newCells(data);
            });
        });
        this.get('#basicImport', function () {
            sendRequest(app.dataModel.newBtsExcelsUrl, "GET", null, function (data) {
                self.newBtss(data);
            });
        });
        this.get('#basicImport', function () {
            sendRequest(app.dataModel.newCdmaCellExcelsUrl, "GET", null, function (data) {
                self.newCdmaCells(data);
            });
        });
        this.get('/Parameters/BasicImport', function () { this.app.runRoute('get', '#basicImport'); });
    });

    return self;
}

app.addViewModel({
    name: "BasicImport",
    bindingMemberName: "basicImport",
    factory: BasicImportViewModel
});