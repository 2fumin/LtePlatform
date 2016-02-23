
var queryBtsLonLatEdits = function(btss) {
    var result = [];
    for (var index = 0; index < btss.length; index++) {
        if (!isLonLatValid(btss[index])) {
            result.push({
                index: index,
                bscId: btss[index].bscId,
                btsId: btss[index].btsId,
                name: btss[index].name,
                districtName: btss[index].districtName,
                longtitute: eNodebs[index].longtitute,
                lattitute: eNodebs[index].lattitute
            });
        }
    }
    return result;
};

var queryCdmaCellLonLatEdits = function(cells) {
    var result = [];
    for (var index = 0; index < cells.length; index++) {
        if (!isLonLatValid(cells[index])) {
            result.push({
                index: index,
                btsId: cells[index].btsId,
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

var postAllENodebs = function(viewModel) {
    if (viewModel.newENodebs().length > 0) {
        sendRequest(app.dataModel.newENodebExcelsUrl, "POST", {
            infos: viewModel.newENodebs()
        }, function(result) {
            viewModel.dumpResultMessage(viewModel.dumpResultMessage() + "完成LTE基站导入" + result + "个；");
            viewModel.newENodebs([]);
        });
    }
};

var postAllBtss = function(viewModel) {
    if (viewModel.newBtss().length > 0) {
        sendRequest(app.dataModel.newBtsExcelsUrl, "POST", {
            infos: viewModel.newBtss()
        }, function(result) {
            viewModel.dumpResultMessage(viewModel.dumpResultMessage() + "完成CDMA基站导入" + result + "个；");
            viewModel.newBtss([]);
        });
    }
};

var postAllCells = function(viewModel) {
    if (viewModel.newCells().length > 0) {
        sendRequest(app.dataModel.newCellExcelsUrl, "POST", {
            infos: viewModel.newCells()
        }, function(result) {
            viewModel.dumpResultMessage(viewModel.dumpResultMessage() + "完成LTE小区导入" + result + "个；");
            viewModel.newCells([]);
        });
    }
};

var postAllCdmaCells = function(viewModel) {
    if (viewModel.newCdmaCells().length > 0) {
        sendRequest(app.dataModel.newCdmaCellExcelsUrl, "POST", {
            infos: viewModel.newCdmaCells()
        }, function(result) {
            viewModel.dumpResultMessage(viewModel.dumpResultMessage() + "完成CDMA小区导入" + result + "个；");
            viewModel.newCdmaCells([]);
        });
    }
};