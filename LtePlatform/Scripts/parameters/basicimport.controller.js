var queryENodebLonLatEdits = function (eNodebs) {
    var result = [];
    for (var index = 0; index < eNodebs.length; index++) {
        if (!isLonLatValid(eNodebs[index])) {
            result.push({
                index: index,
                eNodebId: eNodebs[index].eNodebId,
                name: eNodebs[index].name,
                district: eNodebs[index].districtName,
                town: eNodebs[index].townName,
                longtitute: eNodebs[index].longtitute,
                lattitute: eNodebs[index].lattitute
            });
        }
    }
    return result;
};

var mapLonLat = function (eNodeb, eNodebLonLatEdit) {
    eNodeb.longtitute = eNodebLonLatEdit.longtitute;
    eNodeb.lattitute = eNodebLonLatEdit.lattitute;
};

var mapENodebLonLatEdits = function (eNodebs, eNodebLonLatEdits) {
    for (var i = 0; i < eNodebLonLatEdits.length; i++) {
        if (isLongtituteValid(eNodebLonLatEdits[i])) {
            mapLonLat(eNodebs[eNodebLonLatEdits[i].index], eNodebLonLatEdits[i]);
        }
    }
};