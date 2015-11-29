function BasicImportViewModel(app, dataModel) {
    var self = this;

    return self;
}

app.addViewModel({
    name: "BasicImport",
    bindingMemberName: "basicImport",
    factory: BasicImportViewModel
});