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
            updateDumpHistory(self);
            self.clearItems();
        });
        this.post('#precisePost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            updateDumpHistory(self);
            sendRequest(app.dataModel.dumpAlarmUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });

        this.get('/Parameters/PreciseImport', function () { this.app.runRoute('get', '#preciseImport'); });
        this.get('/Parameters/PrecisePost', function () { this.app.runRoute('post', '#precisePost'); });
    });

    return self;
}

app.addViewModel({
    name: "PreciseImport",
    bindingMemberName: "preciseImport",
    factory: PreciseImportViewModel
});