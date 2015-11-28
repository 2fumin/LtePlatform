function ListViewModel(app, dataModel) {
    var self = this;

    self.result = ko.observable("");

    Sammy(function () {
        this.get('#list', function () {
            self.result("Success!");
        });
        this.get('/Dt/List', function () { this.app.runRoute('get', '#list');})
    });

    return self;
}

app.addViewModel({
    name: "List",
    bindingMemberName: "list",
    factory: ListViewModel
});