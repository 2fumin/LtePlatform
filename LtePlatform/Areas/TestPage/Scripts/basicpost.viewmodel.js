function BasicPostViewModel(app, dataModel) {
    var self = this;

    self.originENodebs = ko.observableArray([{
            address: "佛山市顺德区乐从镇邮政二楼接入机房",
            cityName: "佛山",
            districtName: "顺德",
            divisionDuplex: "FDD",
            eNodebId: 552830,
            factory: "华为",
            gateway: {
                addressString: "8.142.97.1",
                addressValue: 143548673,
                ipByte1: 8,
                ipByte2: 142,
                ipByte3: 97,
                ipByte4: 1
            },
            gatewayString: "8.142.97.1",
            grid: "乐从",
            ip: {
                addressString: "8.142.97.18",
                addressValue: 143548690,
                ipByte1: 8,
                ipByte2: 142,
                ipByte3: 97,
                ipByte4: 18
            },
            ipString: "8.142.97.18",
            lattitute: 22.958611,
            longtitute: 113.082222,
            name: "乐从邮政LBBU6",
            openDate: "0001-01-01T00:00:00",
            planNum: "FSL0614-2-B6",
            townName: "乐从"
        }, {
            address: "佛山市顺德区乐从镇邮政二楼接入机房",
            cityName: "佛山",
            districtName: "顺德",
            divisionDuplex: "FDD",
            eNodebId: 552831,
            factory: "华为",
            gateway: {
                addressString: "8.142.97.1",
                addressValue: 143548673,
                ipByte1: 8,
                ipByte2: 142,
                ipByte3: 97,
                ipByte4: 1
            },
            gatewayString: "8.142.97.1",
            grid: "乐从",
            ip: {
                addressString: "8.142.97.19",
                addressValue: 143548691,
                ipByte1: 8,
                ipByte2: 142,
                ipByte3: 97,
                ipByte4: 19
            },
            ipString: "8.142.97.19",
            lattitute: 22.958611,
            longtitute: 113.082222,
            name: "乐从邮政LBBU7",
            openDate: "0001-01-01T00:00:00",
            planNum: "FSL0614-2-B7",
            townName: "乐从"
        }]);
    self.secondENodebs = ko.observableArray([]);
    self.thirdENodebs = ko.observableArray([]);

    self.postOrigin = function () {
        sendRequest("/api/TestPostOriginENodebs", "POST", {
            infos: self.originENodebs()
        }, function (result) {
            self.secondENodebs(result);
        });
        sendRequest("/api/TestPostBackENodebs", "POST", {
            infos: self.originENodebs()
        }, function (result) {
            self.thirdENodebs(result);
        });
    };

    return self;
}

app.addViewModel({
    name: "BasicPost",
    bindingMemberName: "basicPost",
    factory: BasicPostViewModel
});