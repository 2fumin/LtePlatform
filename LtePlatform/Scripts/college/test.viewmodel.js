function TestViewModel(app, dataModel) {
    var self = this;

    self.colleges = ko.observableArray([]);
    self.selectedCollege = ko.observable();
    self.test3G = ko.observableArray([]);
    self.test4G = ko.observableArray([]);
    self.edit3G = ko.observable();
    self.edit4G = ko.observable();
    self.date = ko.observable((new Date()).Format("yyyy-MM-dd"));
    self.hour = ko.observable(8);
    self.hourSelection = ko.observableArray([8, 10, 12, 14, 16, 18, 20]);
    self.eNodebs = ko.observableArray([]);
    self.selectedENodeb = ko.observable();
    self.sectorSelection = ko.observableArray([]);
    self.sectorId = ko.observable(0);
    
    Sammy(function () {
        this.get('#test', function () {
            $("#date").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCollegeList(self);

            getTestList(self);

            self.selectedCollege.subscribe(function (newCollege) {
                sendRequest(app.dataModel.collegeENodebUrl, "GET", { collegeName: newCollege }, function (data) {
                    self.eNodebs.removeAll();
                    self.eNodebs.push.apply(self.eNodebs, data);
                });
            });

            self.date.subscribe(function () {
                getTestList(self);
            });
            self.hour.subscribe(function () {
                getTestList(self);
            });
            self.selectedENodeb.subscribe(function (newENodeb) {
                updateSectorSelection(newENodeb, self);
            });
        });
        this.get('/College/TestReport', function () { this.app.runRoute('get', '#test'); });
    });

    self.deleteTest3G = function (name) {
        sendRequest(app.dataModel.college3GTestUrl, "GET", {
            recordDate: self.date(),
            hour: self.hour(),
            name: name
        },
            function () {
                update3GList(self);
            });
    };

    self.deleteTest4G = function (name) {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            recordDate: self.date(),
            hour: self.hour(),
            name: name
        },
            function () {
                update4GList(self);
            });
    };

    self.postTest3G = function () {
        sendRequest(app.dataModel.college3GTestUrl, "POST", self.edit3G(), function () {
            $('#edit-3G').modal('hide');
            update3GList(self);
        });
    };

    self.postTest4G = function () {
        sendRequest(app.dataModel.college4GTestUrl, "POST", self.edit4G(), function () {
            $('#edit-4G').modal('hide');
            update4GList(self);
        });
    };

    self.createTest3G = function () {
        sendRequest(app.dataModel.college3GTestUrl, "GET", {
            date: self.date,
            hour: self.hour,
            name: self.selectedCollege
        },
            function (data) {
                self.edit3G(data);
                $('#edit-3G').modal('show');
            });
    };

    self.createTest4G = function () {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            date: self.date,
            hour: self.hour,
            name: self.selectedCollege,
            eNodebName: self.selectedENodeb,
            sectorId: self.sectorId
        },
            function (data) {
                self.edit4G(data);
                $('#edit-4G').modal('show');
            });
    };

    return self;
}

app.addViewModel({
    name: "Test",
    bindingMemberName: "test",
    factory: TestViewModel
});