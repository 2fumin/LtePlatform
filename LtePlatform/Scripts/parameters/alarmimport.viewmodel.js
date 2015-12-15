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
            updateDumpHistory(self);
        });
        this.post('#alarmZtePost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            sendRequest(app.dataModel.dumpAlarmUrl, "GET", function (result) {
                self.totalDumpItems(result);
            });
        });
        this.post('#alarmHwPost', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });
            self.initialize(false);
            sendRequest(app.dataModel.dumpAlarmUrl, "GET", function (result) {
                self.totalDumpItems(result);
            });
        });

        this.get('/Parameters/AlarmImport', function () { this.app.runRoute('get', '#alarmImport'); });
        this.get('/Parameters/ZteAlarmPost', function () { this.app.runRoute('post', '#alarmZtePost'); });
        this.get('/Parameters/HwAlarmPost', function () { this.app.runRoute('post', '#alarmHwPost'); });
    });

    self.dumpItems = function() {
        while (self.totalSuccessItems() + self.totalFailItems() < self.totalDumpItems()) {
            sendRequest(app.dataModel.dumpAlarmUrl, "PUT", null, function(result) {
                if (result === true) {
                    self.totalSuccessItems(self.totalSuccessItems() + 1);
                } else {
                    self.totalFailItems(self.totalFailItems() + 1);
                }
            });
        }
    };

    return self;
}

app.addViewModel({
    name: "AlarmImport",
    bindingMemberName: "alarmImport",
    factory: AlarmImportViewModel
});