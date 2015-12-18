function PreciseImportViewModel(app, dataModel) {
    var self = this;

    self.initialize = ko.observable(true);
    self.totalDumpItems = ko.observable(0);
    self.totalSuccessItems = ko.observable(0);
    self.totalFailItems = ko.observable(0);
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).Format("yyyy-MM-dd"));
    self.dumpHistory = ko.observableArray([]);
    self.townPreciseViews = ko.observableArray([]);

    Sammy(function () {
        this.get('#preciseImport', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(true);
            self.updateHistoryItems();
        });
        this.post('#precisePost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            updateDumpHistory(self);
            sendRequest(app.dataModel.preciseImportUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });

        this.get('/Parameters/PreciseImport', function () { this.app.runRoute('get', '#preciseImport'); });
        this.get('/Parameters/PrecisePost', function () { this.app.runRoute('post', '#precisePost'); });
    });

    self.dumpItems = function () {
        dumpProgressItems(self, app.dataModel.preciseImportUrl);
    };

    self.updateHistoryItems = function () {
        updateDumpHistory(self);
        self.clearItems();
    };

    self.clearItems = function () {
        sendRequest(app.dataModel.preciseImportUrl, "DELETE", null, function () {
            self.totalDumpItems(0);
            self.totalFailItems(0);
            self.totalSuccessItems(0);
        });
    };

    return self;
}

app.addViewModel({
    name: "PreciseImport",
    bindingMemberName: "preciseImport",
    factory: PreciseImportViewModel
});