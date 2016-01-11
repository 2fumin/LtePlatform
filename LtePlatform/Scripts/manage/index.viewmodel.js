function IndexViewModel(app, dataModel) {
    var self = this;

    self.manageUsers = ko.observableArray([]);
    
    Sammy(function () {
        this.get('#index', function () {
            $.ajax({
                method: 'get',
                url: app.dataModel.applicationUsersUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function(data) {
                    self.manageUsers(data);
                }
            });
        });
        this.get('/Manage', function () { this.app.runRoute('get', '#index'); });
    });

    return self;
}

app.addViewModel({
    name: "Index",
    bindingMemberName: "index",
    factory: IndexViewModel
});