var updateSectorSelection = function (name) {
    sendRequest(app.dataModel.cellUrl, "GET", { eNodebName: name }, function (data) {
        app.sectorSelection.removeAll();
        app.sectorSelection.push.apply(app.sectorSelection, data);
    });
};

var getTestList = function () {
    update3GList();
    update4GList();
};

var update3GList = function () {
    sendRequest(app.dataModel.college3GTestUrl, "GET", { date: app.date, hour: app.hour }, function (data) {
        app.test3G.removeAll();
        app.test3G.push.apply(app.test3G, data);
    });
};

var update4GList = function () {
    sendRequest(app.dataModel.college4GTestUrl, "GET", { date: app.date, hour: app.hour }, function (data) {
        app.test4G.removeAll();
        app.test4G.push.apply(app.test4G, data);
    });
};

var deleteTest3G = function (name) {
    sendRequest(app.dataModel.college3GTestUrl, "GET", { recordDate: app.date(), hour: app.hour(), name: name },
        function () { update3GList(); });
};

var deleteTest4G = function (name) {
    sendRequest(app.dataModel.college4GTestUrl, "GET", { recordDate: app.date(), hour: app.hour(), name: name },
        function () { update4GList(); });
};

var postTest3G = function () {
    sendRequest(app.dataModel.college3GTestUrl, "POST", app.edit3G(), function () {
        $('#edit-3G').modal('hide');
        update3GList();
    });
};

var postTest4G = function () {
    sendRequest(app.dataModel.college4GTestUrl, "POST", app.edit4G(), function () {
        $('#edit-4G').modal('hide');
        update4GList();
    });
};

var createTest3G = function () {
    sendRequest(app.dataModel.college3GTestUrl, "GET", { date: app.date, hour: app.hour, name: app.selectedCollege },
        function (data) {
            app.edit3G(data);
            $('#edit-3G').modal('show');
        });
};

var createTest4G = function () {
    sendRequest(app.dataModel.college4GTestUrl, "GET", {
        date: app.date, hour: app.hour, name: app.selectedCollege, eNodebName: app.selectedENodeb, sectorId: app.sectorId
    },
        function (data) {
            app.edit4G(data);
            $('#edit-4G').modal('show');
        });
};