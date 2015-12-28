function WorkItemViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#workItem', function () {
      
        });
        this.get('/Kpi/TopDrop2GDaily', function () { this.app.runRoute('get', '#workItem'); });
    });

    return self;
}

app.addViewModel({
    name: "WorkItem",
    bindingMemberName: "workItem",
    factory: WorkItemViewModel
});