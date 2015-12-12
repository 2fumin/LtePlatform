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
        });
        this.get('/Kpi/PreciseTop', function () { this.app.runRoute('get', '#preciseTop'); });
    });

    self.showStat = function () {

    };

    self.showChart = function () {

    };

    return self;
}

app.addViewModel({
    name: "PreciseTop",
    bindingMemberName: "preciseTop",
    factory: PreciseTopViewModel
});