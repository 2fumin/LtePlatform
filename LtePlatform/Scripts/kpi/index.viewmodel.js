function IndexViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);

    app.initialize = function() {
        // Make a call to the protected Web API by passing in a Bearer Authorization Header
        $.ajax({
            method: 'get',
            url: app.dataModel.cityListUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function(data) {
                app.cities(data);
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