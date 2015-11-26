function InfrastructureViewModel(app, dataModel) {
    var self = this;

    app.setView = ko.observable('list');
    app.currentName = ko.observable();
    app.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    app.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    app.collegeList = ko.observableArray([]);
    app.eNodebList = ko.observableArray([]);
    app.cellList = ko.observableArray([]);
    app.btsList = ko.observableArray([]);
    app.cdmaCellList = ko.observableArray([]);
    app.distributionList = ko.observableArray([]);
    app.alarms = ko.observableArray([]);

    app.initialize = function () {
        $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

        $.ajax({
            method: 'get',
            url: app.dataModel.collegeStatUrl,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (data) {
                app.collegeList(data);
            }
        });
    };

    app.gobackList = function () {
        app.setView('list');
    };

    app.refreshAlarms = function () {
        app.showENodebs(app.currentName());
    };

    app.showENodebs = function (name) {
        sendRequest(app.dataModel.collegeENodebUrl, "GET", {
            collegeName: name,
            begin: app.beginDate(),
            end: app.endDate()
        }, function (data) {
            app.eNodebList(data);
            app.setView('eNodebs');
            app.currentName(name);
        });
    };

    app.showCells = function (name) {
        sendRequest(app.dataModel.collegeCellsUrl, "GET", { collegeName: name }, function (data) {
            app.cellList(data);
            app.setView('cells');
            app.currentName(name);
        });
    };

    app.showBtss = function (name) {
        sendRequest(app.dataModel.collegeBtssUrl, "GET", { collegeName: name }, function (data) {
            app.btsList(data);
            app.setView('btss');
            app.currentName(name);
        });
    };

    app.showCdmaCells = function (name) {
        sendRequest(app.dataModel.collegeCdmaCellsUrl, "GET", { collegeName: name }, function (data) {
            app.cdmaCellList(data);
            app.setView('cdmaCells');
            app.currentName(name);
        });
    };

    app.showLteDistributions = function (name) {
        sendRequest(app.dataModel.collegeLteDistributionsUrl, "GET", { collegeName: name }, function (data) {
            app.distributionList(data);
            app.setView('lteDistributions');
            app.currentName(name);
        });
    };

    app.showCdmaDistributions = function (name) {
        sendRequest(app.dataModel.collegeCdmaDistributionsUrl, "GET", { collegeName: name }, function (data) {
            app.distributionList(data);
            app.setView('cdmaDistributions');
            app.currentName(name);
        });
    };

    app.loadAlarms = function(id) {
        return app.dataModel.loadAlarms(id, app.beginDate(), app.endDate());
    };

    return self;
}

app.addViewModel({
    name: "Infrastructure",
    bindingMemberName: "infrastructure",
    factory: InfrastructureViewModel
});