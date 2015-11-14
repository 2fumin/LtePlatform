﻿function KpiViewModel(app, dataModel) {
    var self = this;

    app.colleges = ko.observableArray([]);
    app.selectedCollege = ko.observable();
    app.kpi = ko.observableArray([]);
    app.edit = ko.observable();
    app.date = ko.observable((new Date()).Format("yyyy-MM-dd"));
    app.hour = ko.observable(8);
    app.hourSelection = ko.observableArray([9, 11, 13, 15, 17, 19, 21]);

    app.initialize = function () {
        $("#date").datepicker({ dateFormat: 'yy-mm-dd' });
        app.date.subscribe(function () { getKpiList(); });
        app.hour.subscribe(function () { getKpiList(); });
        getKpiList();

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
}

app.addViewModel({
    name: "Kpi",
    bindingMemberName: "kpi",
    factory: KpiViewModel
});