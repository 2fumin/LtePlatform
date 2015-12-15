function AlarmImportViewModel(app, dataModel) {
    var self = this;

    self.initialize = ko.observable(true);

    Sammy(function () {
        this.get('#alarmImport', function () {
            self.initialize(true);
        });
        this.post('#alarmZtePost', function() {
            self.initialize(false);
        });
        this.post('#alarmHwPost', function () {
            self.initialize(false);
        });

        this.get('/Parameters/AlarmImport', function () { this.app.runRoute('get', '#alarmImport'); });
        this.get('/Parameters/ZteAlarmPost', function () { this.app.runRoute('post', '#alarmZtePost'); });
        this.get('/Parameters/HwAlarmPost', function () { this.app.runRoute('post', '#alarmHwPost'); });
    });

    return self;
}

app.addViewModel({
    name: "AlarmImport",
    bindingMemberName: "alarmImport",
    factory: AlarmImportViewModel
});