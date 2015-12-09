function QueryMapViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#queryMap', function () {

            initializeMap('mapContent', 11);
        });
        this.get('/Parameters/QueryMap', function () { this.app.runRoute('get', '#queryMap'); });
    });
    return self;
}

app.addViewModel({
    name: "QueryMap",
    bindingMemberName: "queryMap",
    factory: QueryMapViewModel
});