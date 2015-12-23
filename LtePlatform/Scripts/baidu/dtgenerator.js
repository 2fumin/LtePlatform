function DtGenerator() {
    var self = this;
    self.defaultRsrpCriteria = [
    {
        threshold: -120,
        color: "ff0000"
    }, {
        threshold: -115,
        color: "7f0808"
    }, {
        threshold: -110,
        color: "3f0f0f"
    }, {
        threshold: -105,
        color: "077f7f"
    }, {
        threshold: -95,
        color: "07073f"
    }, {
        threshold: -80,
        color: "073f07"
    }];
    self.defaultSinrCriteria = [
    {
        threshold: -3,
        color: "ff0000"
    }, {
        threshold: 0,
        color: "7f0808"
    }, {
        threshold: 3,
        color: "3f0f0f"
    }, {
        threshold: 6,
        color: "077f7f"
    }, {
        threshold: 9,
        color: "07073f"
    }, {
        threshold: 15,
        color: "073f07"
    }];
    self.defaultRxCriteria = [
    {
        threshold: -110,
        color: "ff0000"
    }, {
        threshold: -105,
        color: "7f0808"
    }, {
        threshold: -100,
        color: "3f0f0f"
    }, {
        threshold: -95,
        color: "077f7f"
    }, {
        threshold: -85,
        color: "07073f"
    }, {
        threshold: -70,
        color: "073f07"
    }];
    self.defaultSinr3GCriteria = [
    {
        threshold: -9,
        color: "ff0000"
    }, {
        threshold: -6,
        color: "7f0808"
    }, {
        threshold: -3,
        color: "3f0f0f"
    }, {
        threshold: 0,
        color: "077f7f"
    }, {
        threshold: 3,
        color: "07073f"
    }, {
        threshold: 7,
        color: "073f07"
    }];
    self.defaultEcioCriteria = [
    {
        threshold: -15,
        color: "ff0000"
    }, {
        threshold: -12,
        color: "7f0808"
    }, {
        threshold: -9,
        color: "3f0f0f"
    }, {
        threshold: -7,
        color: "077f7f"
    }, {
        threshold: -5,
        color: "07073f"
    }, {
        threshold: -3,
        color: "073f07"
    }];
    self.defaultTxCriteria = [
    {
        threshold: 12,
        color: "ff0000"
    }, {
        threshold: 6,
        color: "7f0808"
    }, {
        threshold: 0,
        color: "3f0f0f"
    }, {
        threshold: -3,
        color: "077f7f"
    }, {
        threshold: -6,
        color: "07073f"
    }, {
        threshold: -12,
        color: "073f07"
    }];
}

DtGenerator.prototype.generateRsrpPoints = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.rsrp < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateRx0Points = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.rxAgc0 < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateRx1Points = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.rxAgc1 < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateSinrPoints = function(coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.sinr < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateEcioPoints = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.ecio < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateRxPoints = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.rxAgc < criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};

DtGenerator.prototype.generateTxPoints = function (coverageData, criteria) {
    var radius = getDtPointRadius(map.getZoom());
    for (var i = 0; i < coverageData.length; i++) {
        var color = "077f07";
        var data = coverageData[i];
        for (var j = 0; j < criteria.length; j++) {
            if (data.txPower > criteria[j].threshold) {
                color = criteria[j].color;
                break;
            }
        }
        addOneDtPoint(data.baiduLongtitute, data.baiduLattitute, color, radius);
    }
};