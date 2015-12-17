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

    self.showRates = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            upload: 1
        }, function(download) {
            var downloadRates = matchCollegeStats(self.collegeNames(), download);
            sendRequest(app.dataModel.college4GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                upload: 0
            }, function(upload) {
                var uploadRates = matchCollegeStats(self.collegeNames(), upload);
                sendRequest(app.dataModel.college3GTestUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate()
                }, function(evdo) {
                    var evdoRates = matchCollegeStats(self.collegeNames(), evdo);
                    showCollegeRates(self.collegeNames(), downloadRates, uploadRates, evdoRates, "#college-rates");
                });
            });
        });
    };

    self.showUsers = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "users"
        }, function(lte) {
            var lteUsers = matchCollegeStats(self.collegeNames(), lte);
            sendRequest(app.dataModel.college3GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "users"
            }, function(evdo) {
                var evdoUsers = matchCollegeStats(self.collegeNames(), evdo);
                showCollegeUsers(self.collegeNames(), lteUsers, evdoUsers, "#college-users");
            });
        });
    };

    self.showCoverage = function() {
        sendRequest(app.dataModel.college4GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "rsrp"
        }, function(rsrp) {
            var rsrpStats = matchCollegeStats(self.collegeNames(), rsrp);
            sendRequest(app.dataModel.college4GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "sinr"
            }, function(sinr) {
                var sinrStats = matchCollegeStats(self.collegeNames(), sinr);
                showCollegeCoverage(self.collegeNames(), rsrpStats, sinrStats, "#college-coverage");
            });
        });
    };

    self.showInterference = function() {
        sendRequest(app.dataModel.college3GTestUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate(),
            kpiName: "minRssi"
        }, function(minRssi) {
            var minRssiStats = matchCollegeStats(self.collegeNames(), minRssi);
            sendRequest(app.dataModel.college3GTestUrl, "GET", {
                begin: self.beginDate(),
                end: self.endDate(),
                kpiName: "maxRssi"
            }, function(maxRssi) {
                var maxRssiStats = matchCollegeStats(self.collegeNames(), maxRssi);
                sendRequest(app.dataModel.college3GTestUrl, "GET", {
                    begin: self.beginDate(),
                    end: self.endDate(),
                    kpiName: "vswr"
                }, function(vswr) {
                    var vswrStats = matchCollegeStats(self.collegeNames(), vswr);
                    showCollegeInterference(self.collegeNames(), minRssiStats, maxRssiStats, vswrStats, "#college-interference");
                });
            });
        });
    };

    self.showAlarms = function() {
        sendRequest(app.dataModel.collegeAlarmUrl, "GET", {
            begin: self.beginDate(),
            end: self.endDate()
        }, function(results) {
            var chart = new DrilldownColumn();
            chart.title.text = "校园网告警分布图";
            chart.series[0].data = [];
            chart.drilldown.series = [];
            chart.series[0].name = "校园名称";
            chart.options.yAxis = {
                title: {
                    text: '告警总数'
                }

            };
            for (var i = 0; i < self.collegeNames().length; i++) {
                var collegeName = self.collegeNames()[i];
                if (results[collegeName] !== undefined) {
                    var collegeAlarms = 0;
                    var subData = [];
                    for (var j = 0; j < results[collegeName].length; j++) {
                        collegeAlarms += results[collegeName][j].item2;
                        subData.push([results[collegeName][j].item1, results[collegeName][j].item2]);
                    }
                    chart.addOneSeries(collegeName, collegeAlarms, subData);
                }
            }
            showChartDialog("#college-alarms", chart);
        });
    };

    self.showFlows = function() {

    };

    self.showConnection = function() {

    };

    self.showDrop = function() {

    };

    self.toggleCollegeMarkers = function () {
        toggleDisplay(map.collegeMarkers);
    };

    self.toggleCollegeENodebs = function() {
        toggleDisplay(map.eNodebMarkers);
    };

    self.toggleCollegeCells = function() {
        toggleDisplay(map.lteSectors);
    };

    self.toggleCollegeLteDistributions = function() {
        toggleDisplay(map.lteDistributions);
    };

    self.toggleCollegeBtss = function() {
        toggleDisplay(map.btsMarkers);
    };

    self.toggleCollegeCdmaCells = function() {
        toggleDisplay(map.cdmaSectors);
    };

    self.toggleCollegeCdmaDistributions = function() {
        toggleDisplay(map.cdmaDistributions);
    };

    self.focusCollege = function (name) {
        for (var i = 0; i < self.collegeInfos().length; i++) {
            if (self.collegeInfos()[i].name === name) {
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