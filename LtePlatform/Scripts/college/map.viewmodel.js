function MapViewModel(app, dataModel) {
    var self = this;

    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.collegeInfos = ko.observableArray([]);
    self.collegeNames = ko.observableArray([]);

    Sammy(function () {
        this.get('#collegeMap', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeMap('all-map', 11);
            map.addEventListener("zoomend", function() {
                if (map.getZoom() > 13) {
                    drawCollegeENodebs(self);
                    drawCollegeBtss(self);
                    drawCollegeCells(self);
                    drawCollegeCdmaCells(self);
                    drawCollegeLteDistributions(self);
                    drawCollegeCdmaDistributions(self);
                }
            });

            $.ajax({
                method: 'get',
                url: app.dataModel.collegeQueryUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    drawCollegeMap(self, data);
                    for (var i = 0; i < data.length; i++) {
                        self.collegeNames.push(data[i].name);
                    }
                }
            });
        });
        this.get('/College/Map', function () { this.app.runRoute('get', '#collegeMap'); });
    });

    self.refresh = function () { };

    self.toggleCollegeMarkers = function () {
        toggleDisplay(map.collegeMarkers);
    };

    self.focusCollege = function (name) {
        for (var i = 0; i < self.collegeInfos().length; i++) {
            if (self.collegeInfos()[i].name == name) {
                var cell = {
                    baiduLongtitute: self.collegeInfos()[i].centerx,
                    baiduLattitute: self.collegeInfos()[i].centery
                };
                setCellFocus(cell);
                break;
            }
        }
    };

    return self;
}

app.addViewModel({
    name: "Map",
    bindingMemberName: "collegeMap",
    factory: MapViewModel
});