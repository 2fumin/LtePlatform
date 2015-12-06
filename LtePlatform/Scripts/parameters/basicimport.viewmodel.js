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
    self.newCellLonLatEdits = ko.observableArray([]);
    self.newBtsLonLatEdits = ko.observableArray([]);
    self.newCdmaCellLonLatEdits = ko.observableArray([]);
    self.dumpResultMessage = ko.observable("");

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
        mapLonLatEdits(self.newENodebs(), self.newENodebLonLatEdits());
        $('#eNodeb-lon-lat').modal('hide');
    };

    self.checkCellsLonLat = function() {
        self.newCellLonLatEdits(queryCellLonLatEdits(self.newCells()));
        $('#cell-lon-lat').modal('show');
    };

    self.postCellLonLat=function() {
        mapLonLatEdits(self.newCells(), self.newCellLonLatEdits());
        $('#cell-lon-lat').modal('hide');
    }

    self.checkBtssLonLat = function() {
        self.newBtsLonLatEdits(queryBtsLonLatEdits(self.newBtss()));
        $('#bts-lon-lat').modal('show');
    };

    self.postBtsLonLat = function() {
        mapLonLatEdits(self.newBtss(), self.newBtsLonLatEdits());
        $('#bts-lon-lat').modal('hide');
    };

    self.checkCdmaCellsLonLat = function() {
        self.newCdmaCellLonLatEdits(queryCdmaCellLonLatEdits(self.newCdmaCells()));
        $('#cdmaCell-lon-lat').modal('show');
    };

    self.postCdmaCellLonLat = function() {
        mapLonLatEdits(self.newCdmaCells(), self.newCdmaCellLonLatEdits());
        $('#cdmaCell-lon-lat').modal('hide');
    };

    self.postAll = function () {
        if (self.newENodebs().length > 0) {
            sendRequest(app.dataModel.newENodebExcelsUrl, "POST", JSON.stringify(self.newENodebs()), function (result) {
                //self.dumpResultMessage(self.dumpResultMessage() + "完成LTE基站导入；");
                alert(result);
            });
        }
        //if (self.newBtss().length > 0) {
        //    sendRequest(app.dataModel.newBtsExcelsUrl, "POST", JSON.stringify(self.newBtss()), function() {
        //        self.dumpResultMessage(self.dumpResultMessage() + "完成CDMA基站导入；");
        //    });
        //}
        //if (self.newCells().length > 0) {
        //    sendRequest(app.dataModel.newCellExcelsUrl, "POST", JSON.stringify(self.newCells()), function() {
        //        self.dumpResultMessage(self.dumpResultMessage() + "完成LTE小区导入；");
        //    });
        //}
        //if (self.newCdmaCells().length > 0) {
        //    sendRequest(app.dataModel.newCdmaCellExcelsUrl, "POST", JSON.stringify(self.newCdmaCells()), function() {
        //        self.dumpResultMessage(self.dumpResultMessage() + "完成CDMA小区导入；");
        //    });
        //}
    };

    return self;
}

app.addViewModel({
    name: "BasicImport",
    bindingMemberName: "basicImport",
    factory: BasicImportViewModel
});