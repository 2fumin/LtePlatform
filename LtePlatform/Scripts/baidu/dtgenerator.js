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