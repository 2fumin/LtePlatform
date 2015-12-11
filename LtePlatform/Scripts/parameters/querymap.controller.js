var drawENodebs = function(viewModel, options) {
    sendRequest(app.dataModel.eNodebUrl, "GET", options, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneENodebMarker(result[i]);
            sendRequest(app.dataModel.cellUrl, "GET", {
                eNodebId: result[i].eNodebId
            }, function(cells) {
                for (var j = 0; j < cells.length; j++) {
                    addOneGeneralSector(cells[j], "LteCell");
                }
            });
        }
    }, function (result) {
        alert(getErrorMessage(result));
    });
};

var queryENodebs = function (viewModel) {
    removeAllENodebs();
    removeAllLteSectors();
    if (viewModel.queryText().trim() === "") {
        drawENodebs(viewModel, {
            city: viewModel.currentCity(),
            district: viewModel.currentDistrict(),
            town: viewModel.currentTown()
        });
    } else {
        drawENodebs(viewModel, {
            name: viewModel.queryText()
        });
    }
};

var drawBtss= function(viewModel, options) {
    sendRequest(app.dataModel.btsUrl, "GET", options, function (result) {
        for (var i = 0; i < result.length; i++) {
            addOneBtsMarker(result[i]);
        }
    }, function (result) {
        alert(getErrorMessage(result));
    });
}

var queryBtss = function(viewModel) {
    removeAllBtss();
    if (viewModel.queryText().trim() === "") {
        drawBtss(viewModel, {
            city: viewModel.currentCity(),
            district: viewModel.currentDistrict(),
            town: viewModel.currentTown()
        });
    } else {
        drawBtss(viewModel, {
            name: viewModel.queryText()
        });
    }
};