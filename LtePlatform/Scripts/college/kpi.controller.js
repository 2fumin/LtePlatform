var getKpiList = function () {
    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
         date: app.date, hour: app.hour
    }, function (data) {
        app.kpis.removeAll();
        app.kpis.push.apply(app.kpis, data);
    });
};

var deleteKpi = function (name) {
    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
             recordDate: app.date(), hour: app.hour(), name: name
        },
        function () { getKpiList(); });
};

var postKpi = function () {
    sendRequest(app.dataModel.collegeKpiUrl, "POST", app.edit(), function () {
        $('#edit').modal('hide');
        getKpiList();
    });
};

var createKpi = function () {
    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
             date: app.date, hour: app.hour, name: app.selectedCollege
        },
        function (data) {
            app.edit(data);
            $('#edit').modal('show');
        });
};