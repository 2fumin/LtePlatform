function KpiViewModel(app, dataModel) {
    var self = this;

    self.colleges = ko.observableArray([]);
    self.selectedCollege = ko.observable();
    self.kpis = ko.observableArray([]);
    self.edit = ko.observable();
    self.date = ko.observable((new Date()).Format("yyyy-MM-dd"));
    self.hour = ko.observable(8);
    self.hourSelection = ko.observableArray([9, 11, 13, 15, 17, 19, 21]);

    Sammy(function () {
        this.get('#kpi', function () {
            $("#date").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCollegeList(self);

            self.date.subscribe(function () { getKpiList(self); });
            self.hour.subscribe(function () { getKpiList(self); });
        });
        this.get('/College/KpiReport', function () { this.app.runRoute('get', '#kpi'); });
    });

    self.deleteKpi = function (view) {
        sendRequest(app.dataModel.collegeKpiUrl, "DELETE", view,
            function () { getKpiList(self); });
    };

    self.postKpi = function () {
        sendRequest(app.dataModel.collegeKpiUrl, "POST", self.edit(), function () {
            $('#edit').modal('hide');
            getKpiList(self);
        });
    };

    self.createKpi = function () {
        sendRequest(app.dataModel.collegeKpiUrl, "GET", {
            date: self.date, hour: self.hour, name: self.selectedCollege
        },
            function (data) {
                self.edit(data);
                $('#edit').modal('show');
            });
    };

    return self;
}

app.addViewModel({
    name: "Kpi",
    bindingMemberName: "kpi",
    factory: KpiViewModel
});