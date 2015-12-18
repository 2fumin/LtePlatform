function AlarmImportViewModel(app, dataModel) {
    var self = this;

    self.initialize = ko.observable(true);
    self.totalDumpItems = ko.observable(0);
    self.totalSuccessItems = ko.observable(0);
    self.totalFailItems = ko.observable(0);
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).Format("yyyy-MM-dd"));
    self.dumpHistory = ko.observableArray([]);

    Sammy(function () {
        this.get('#alarmImport', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(true);
            self.updateHistoryItems();
        });
        this.post('#alarmZtePost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            updateDumpHistory(self);
            sendRequest(app.dataModel.dumpAlarmUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });
        this.post('#alarmHwPost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            updateDumpHistory(self);
            sendRequest(app.dataModel.dumpAlarmUrl, "GET", null, function (result) {
                self.totalDumpItems(result);
            });
        });

        this.get('/Parameters/AlarmImport', function () { this.app.runRoute('get', '#alarmImport'); });
        this.get('/Parameters/ZteAlarmPost', function () { this.app.runRoute('post', '#alarmZtePost'); });
        this.get('/Parameters/HwAlarmPost', function () { this.app.runRoute('post', '#alarmHwPost'); });
    });

    self.dumpItems = function () {
        dumpProgressItems(self, app.dataModel.dumpAlarmUrl);
    };

    self.updateHistoryItems = function() {
        updateDumpHistory(self);
        self.clearItems();
    };

    self.clearItems = function() {
        sendRequest(app.dataModel.dumpAlarmUrl, "DELETE", null, function() {
            self.totalDumpItems(0);
            self.totalFailItems(0);
            self.totalSuccessItems(0);
        });
    };

    return self;
}

app.addViewModel({
    name: "AlarmImport",
    bindingMemberName: "alarmImport",
    factory: AlarmImportViewModel
});