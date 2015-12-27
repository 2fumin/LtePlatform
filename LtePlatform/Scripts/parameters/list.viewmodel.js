function ListViewModel(app, dataModel) {
    var self = this;
    
    Sammy(function () {
        this.get('#list', function() {
        });
        this.get('/Parameters/List', function () { this.app.runRoute('get', '#list'); });
    });

    return self;
}

app.addViewModel({
    name: "List",
    bindingMemberName: "list",
    factory: ListViewModel
});