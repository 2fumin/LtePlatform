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

var mapENodebLonLatEdits = function (eNodebs, eNodebLonLatEdits) {
    for (var i = 0; i < eNodebLonLatEdits.length; i++) {
        if (isLongtituteValid(eNodebLonLatEdits[i])) {
            mapLonLat(eNodebs[eNodebLonLatEdits[i].index], eNodebLonLatEdits[i]);
        }
    }
};

var queryCellLonLatEdits = function(cells) {
    var result = [];
    for (var index = 0; index < cells.length; index++) {
        if (!isLonLatValid(cells[index])) {
            result.push({
                index: index,
                eNodebId: cells[index].eNodebId,
                sectorId: cells[index].sectorId,
                frequency: cells[index].frequency,
                isIndoor: cells[index].isIndoor,
                longtitute: cells[index].longtitute,
                lattitute: cells[index].lattitute
            });
        }
    }
    return result;
};

var mapCellLonLatEdits = function(cells, cellLonLatEdits) {
    for (var i = 0; i < cellLonLatEdits.length; i++) {
        mapLonLat(cells[cellLonLatEdits[i].index], cellLonLatEdits[i]);
    }
};

var queryBtsLonLatEdits=function(btss) {
    var result = [];
    for (var index = 0; index < btss.length; index++) {
        if (!isLonLatValid(btss[index])) {
            result.push({
                index: index
            });
        }
    }
    return result;
}