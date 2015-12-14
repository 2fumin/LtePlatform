function AlarmImportViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#alarmImport', function () {
            console.log("Here's import");
        });
        this.post('#alarmZtePost', function() {
            console.log("Here's zte post");
        });
        this.post('#alarmHwPost', function () {
            console.log("Here's hw post");
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