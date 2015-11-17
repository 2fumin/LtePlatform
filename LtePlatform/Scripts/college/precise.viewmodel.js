function PreciseViewModel(app, dataModel) {
    var self = this;

    app.colleges = ko.observableArray([]);
    app.selectedCollege = ko.observable();
    app.startDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.cellList = ko.observableArray([]);

    app.initialize = function () {
        $("#StartDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        $.ajax({
            method: 'get',
            url: app.dataModel.collegeQueryUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                app.colleges.removeAll();
                for (var i = 0; i < data.length; i++) {
                    app.colleges.push(data[i].name);
                }
            }
        });
    };

    return self;
}

app.addViewModel({
    name: "Precise",
    bindingMemberName: "precise",
    factory: PreciseViewModel
});