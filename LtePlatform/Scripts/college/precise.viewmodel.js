function PreciseViewModel(app, dataModel) {
    var self = this;

    self.colleges = ko.observableArray([]);
    self.selectedCollege = ko.observable();
    self.startDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.cellList = ko.observableArray([]);

    Sammy(function () {
        this.get('#precise', function () {
            $("#StartDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCollegeList(self);
        });
        this.get('/College/PreciseKpi', function () { this.app.runRoute('get', '#precise'); });
    });

    self.queryCells = function () {
        sendRequest(app.dataModel.collegePreciseUrl, "GET", {
            collegeName: self.selectedCollege(),
            begin: self.startDate(),
            end: self.endDate()
        }, function (data) {
            self.cellList(data);
        });
    };

    self.queryPrecise = function (cell) {
        queryPreciseChart(self, cell, "#dialog-modal");
    };

    return self;
}

app.addViewModel({
    name: "Precise",
    bindingMemberName: "precise",
    factory: PreciseViewModel
});