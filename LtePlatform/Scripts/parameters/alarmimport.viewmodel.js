function AlarmImportViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#alarmImport', function () {
            console.log("Here's import");
        });
        this.post('#alarmPost', function() {
            console.log("Here's post");
        });

        this.get('/Parameters/AlarmImport', function () { this.app.runRoute('get', '#alarmImport'); });
        this.get('/Parameters/AlarmPost', function () { this.app.runRoute('post', '#alarmPost'); });
    });

    return self;
}

app.addViewModel({
    name: "AlarmImport",
    bindingMemberName: "alarmImport",
    factory: AlarmImportViewModel
});