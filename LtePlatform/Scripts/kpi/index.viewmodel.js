function IndexViewModel(app, dataModel) {
    var self = this;

    app.currentCity = ko.observable();
    app.cities = ko.observableArray([]);
    app.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.view = ko.observable('主要');
    app.viewOptions = ko.observableArray(['主要', '2G', '3G']);
    app.kpiDateList = ko.observableArray([]);

    app.initialize = function () {
        $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });

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