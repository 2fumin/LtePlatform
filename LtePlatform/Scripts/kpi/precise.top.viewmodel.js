function PreciseTopViewModel(app, dataModel) {
    var self = this;

    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.orderPolicy = ko.observable("");
    self.policySelection = ko.observableArray([]);
    self.topCount = ko.observable(10);
    self.topCountSelection = ko.observableArray([10, 20, 30, 50]);
    self.cellViews = ko.observableArray([]);

    Sammy(function () {
        this.get('#preciseTop', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            $.ajax({
                method: 'get',
                url: app.dataModel.preciseStatUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.policySelection(data);
                }
            });

            initializeMap("#all-map", 12);
        });
        this.get('/Kpi/PreciseTop', function () { this.app.runRoute('get', '#preciseTop'); });
    });

    self.showStat = function () {
        self.cellViews([]);
        sendRequest(app.dataModel.preciseStatUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            topCount: self.topCount(),
            orderSelection: self.orderPolicy()
        }, function (result) {
            self.cellViews(result);
        });
    };

    self.showChart = function () {

    };

    self.queryTrend = function (cell) {
        queryPreciseChart(self, cell, "#dialog-modal");
    };

    return self;
}

app.addViewModel({
    name: "PreciseTop",
    bindingMemberName: "preciseTop",
    factory: PreciseTopViewModel
});