function CoverageViewModel(app, dataModel) {
    var self = this;

    self.beginDate = ko.observable((new Date()).getDateFromToday(-7).Format("yyyy-MM-dd"));
    self.endDate = ko.observable((new Date()).getDateFromToday(-1).Format("yyyy-MM-dd"));
    self.collegeNames = ko.observableArray([]);
    self.collegeInfos = ko.observableArray([]);
    self.selectedCollege = ko.observable();
    self.networkType = ko.observable("4G");
    self.networkList = ko.observableArray(["2G", "3G", "4G"]);
    self.includeAllFiles = ko.observable(true);
    self.dataFile = ko.observable();
    self.dataFileList = ko.observableArray([]);
    self.rasterFileList = ko.observableArray([]);
    self.coverageKpiList = ko.observableArray([]);
    self.dtGenerator = new DtGenerator();
    self.show4GResults = ko.observable(false);
    self.show3GResults = ko.observable(false);
    self.show2GResults = ko.observable(false);

    Sammy(function () {
        this.get('#collegeCoverage', function () {
            $("#BeginDate").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#EndDate").datepicker({ dateFormat: 'yy-mm-dd' });

            initializeMap('all-map', 11);
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
        this.get('/College/Coverage', function () { this.app.runRoute('get', '#collegeCoverage'); });
    });

    self.showDataFile = function() {
        var fileList = [];
        sendRequest(app.dataModel.collegeRegionUrl, "GET", {
            collegeName: self.selectedCollege()
        }, function(result) {
            sendRequest(app.dataModel.rasterFileUrl, "GET", {
                dataType: self.networkType(),
                west: result.west,
                east: result.east,
                south: result.south,
                north: result.north,
                begin: self.beginDate(),
                end: self.endDate()
            }, function(list) {
                self.rasterFileList(list);
                for (var i = 0; i < list.length; i++) {
                    fileList.push(list[i].csvFileName);
                }
                self.dataFileList(fileList);
                if (fileList.length > 0) {
                    self.dataFile(fileList[0]);
                }
            });
        });
    };

    self.showResults = function() {
        if (self.dataFileList().length === 0) return;
        for (var i = 0; i < self.collegeInfos().length; i++) {
            if (self.collegeInfos()[i].name === self.selectedCollege()) {
                var cell = {
                    baiduLongtitute: self.collegeInfos()[i].centerx,
                    baiduLattitute: self.collegeInfos()[i].centery
                };
                setCellFocus(cell);
                break;
            }
        }
        var url;
        switch (self.networkType()) {
        case "4G":
            url = app.dataModel.record4GUrl;
            self.show2GResults(false);
            self.show3GResults(false);
            self.show4GResults(true);
            break;
        case "3G":
            url = app.dataModel.record3GUrl;
            self.show2GResults(false);
            self.show3GResults(true);
            self.show4GResults(false);
            break;
        default:
            url = app.dataModel.record2GUrl;
            self.show2GResults(true);
            self.show3GResults(false);
            self.show4GResults(false);
            break;
        }
        self.calculateCoverageResults(0, url, []);
    };

    self.calculateCoverageResults = function(index, url, originList) {
        if (index === self.dataFileList().length) {
            self.coverageKpiList(originList);
        } else if (self.dataFile() === self.dataFileList()[index] || self.includeAllFiles()) {
            sendRequest(url, "POST", self.rasterFileList()[index], function(result) {
                self.calculateCoverageResults(index + 1, url, originList.concat(result));
            }, function() {
                self.calculateCoverageResults(index + 1, url, originList);
            });
        } else {
            self.calculateCoverageResults(index + 1, url, originList);
        }
    };

    self.showRsrp = function () {
        clearAllDtPoints();
        self.dtGenerator.generateRsrpPoints(self.coverageKpiList(), self.dtGenerator.defaultRsrpCriteria);
    };

    self.showSinr = function() {
        clearAllDtPoints();
    };

    self.showSinr3G = function() {
        clearAllDtPoints();
    };

    self.showRxAgc0 = function() {
        clearAllDtPoints();
    };

    self.showRxAgc1 = function () {
        clearAllDtPoints();
    };

    self.showEcio = function() {
        clearAllDtPoints();
    };

    self.showRxAgc2G = function() {
        clearAllDtPoints();
    };

    self.showTxPower = function() {
        clearAllDtPoints();
    };

    return self;
}

app.addViewModel({
    name: "Coverage",
    bindingMemberName: "collegeCoverage",
    factory: CoverageViewModel
});