function IndexViewModel(app, dataModel) {
    var self = this;

    self.manageUsers = ko.observableArray([]);
    self.manageRoles = ko.observableArray([]);
    self.roleName = ko.observable("");
    
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
            self.updateRoleList();
        });
        this.get('/Manage', function () { this.app.runRoute('get', '#index'); });
    });

    self.updateRoleList = function() {
        $.ajax({
            method: 'get',
            url: app.dataModel.applicationRolesUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function(data) {
                self.manageRoles(data);
                self.roleName("New role " + data.length);
            }
        });
    };

    self.addRole = function() {
        $.ajax({
            method: "get",
            url: app.dataModel.applicationRolesUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            data: {
                roleName: self.roleName()
            },
            success: function(result) {
                if (result === true) {
                    self.updateRoleList();
                }
            }
        });
    };

    return self;
}

app.addViewModel({
    name: "Index",
    bindingMemberName: "index",
    factory: IndexViewModel
});