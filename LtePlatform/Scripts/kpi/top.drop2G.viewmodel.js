function TopDrop2GViewModel(app, dataModel) {
    var self = this;

    self.currentCity = ko.observable();
    self.cities = ko.observableArray([]);
    self.statDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.oneDayCells = ko.observableArray([]);
    self.trendCells = ko.observableArray([]);
    self.orderPolicy = ko.observable("");
    self.policySelection = ko.observableArray([]);
    self.topCount = ko.observable(10);
    self.topCountSelection = ko.observableArray([10, 20, 30, 50]);

    Sammy(function () {
        this.get('#topDrop2G', function () {
            $("#StatDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeCityKpi(self);

            $.ajax({
                method: 'get',
                url: app.dataModel.topDrop2GUrl,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    self.policySelection(data);
                }
            });
        });
        this.get('/Kpi/TopDrop2G', function () { this.app.runRoute('get', '#topDrop2G'); })
    });

    self.showKpi = function () {
        sendRequest(app.dataModel.topDrop2GUrl, "GET", {
            statDate: self.statDate(),
            city: self.currentCity()
        }, function (data) {
            self.statDate(data.statDate);
            self.oneDayCells(data.statViews);
        });
    }

    self.showHistory = function () {

    };

    self.trendTable = function () {
        sendRequest(app.dataModel.topDrop2GUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            city: self.currentCity(),
            policy: self.orderPolicy(),
            topCount: self.topCount()
        }, function (data) {
            self.trendCells(data);
        });
    };

    self.trendChart = function () {

    };

    return self;
}

app.addViewModel({
    name: "TopDrop2G",
    bindingMemberName: "topDrop2G",
    factory: TopDrop2GViewModel
});