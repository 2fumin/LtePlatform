function BasicImportViewModel(app, dataModel) {
    var self = this;

    self.newENodebs = ko.observableArray([]);
    self.newCells = ko.observableArray([]);
    self.newBtss = ko.observableArray([]);
    self.newCdmaCells = ko.observableArray([]);
    self.newENodebsImport = ko.observable(true);
    self.newCellsImport = ko.observable(true);
    self.newBtssImport = ko.observable(true);
    self.newCdmaCellsImport = ko.observable(true);

    self.newENodebLonLatEdits = ko.observableArray([]);

    Sammy(function () {
        this.get('#basicImport', function () {
            sendRequest(app.dataModel.newENodebExcelsUrl, "GET", null, function(data) {
                self.newENodebs(data);
            });
            sendRequest(app.dataModel.newCellExcelsUrl, "GET", null, function (data) {
                self.newCells(data);
            });
            sendRequest(app.dataModel.newBtsExcelsUrl, "GET", null, function (data) {
                self.newBtss(data);
            });
            sendRequest(app.dataModel.newCdmaCellExcelsUrl, "GET", null, function (data) {
                self.newCdmaCells(data);
            });
        });
        this.get('/Parameters/BasicImport', function () { this.app.runRoute('get', '#basicImport'); });
    });

    self.checkENodebsLonLat = function() {
        self.newENodebLonLatEdits(queryENodebLonLatEdits(self.newENodebs()));
        $('#eNodeb-lon-lat').modal('show');
    };

    self.postENodebLonLat = function() {
        mapENodebLonLatEdits(self.newENodebs(), self.newENodebLonLatEdits());
        $('#eNodeb-lon-lat').modal('hide');
    };

    return self;
}

app.addViewModel({
    name: "BasicImport",
    bindingMemberName: "basicImport",
    factory: BasicImportViewModel
});