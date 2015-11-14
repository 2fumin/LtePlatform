function TestViewModel(app, dataModel) {
    var self = this;

    app.colleges = ko.observableArray([]);
    app.selectedCollege = ko.observable();
    app.test3G = ko.observableArray([]);
    app.test4G = ko.observableArray([]);
    app.edit3G = ko.observable();
    app.edit4G = ko.observable();
    app.date = ko.observable((new Date()).Format("yyyy-MM-dd"));
    app.hour = ko.observable(8);
    app.hourSelection = ko.observableArray([8, 10, 12, 14, 16, 18, 20]);
    app.eNodebs = ko.observableArray([]);
    app.selectedENodeb = ko.observable();
    app.sectorSelection = ko.observableArray([]);
    app.sectorId = ko.observable(0);

    app.initialize = function () {
        $("#date").datepicker({ dateFormat: 'yy-mm-dd' });

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

        getTestList();

        app.selectedCollege.subscribe(function (newCollege) {
            sendRequest(app.dataModel.collegeENodebUrl, "GET", { collegeName: newCollege }, function (data) {
                app.eNodebs.removeAll();
                app.eNodebs.push.apply(app.eNodebs, data);
            });
        });

        app.date.subscribe(function () { getTestList(); });
        app.hour.subscribe(function () { getTestList(); });
        app.selectedENodeb.subscribe(function (newENodeb) { updateSectorSelection(newENodeb); });
    };
}

app.addViewModel({
    name: "Test",
    bindingMemberName: "test",
    factory: TestViewModel
});