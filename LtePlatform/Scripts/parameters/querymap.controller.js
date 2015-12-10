var queryENodebs = function (viewModel) {
    removeAllENodebs();
    if (viewModel.queryText().trim() === "") {
        sendRequest(app.dataModel.eNodebUrl, "GET", {
            city: viewModel.currentCity(),
            district: viewModel.currentDistrict(),
            town: viewModel.currentTown()
        }, function (result) {
            for (var i = 0; i < result.length; i++) {
                addOneENodebMarker(result[i]);
            }
        }, function (result) {
            alert(getErrorMessage(result));
        });
    } else {
        sendRequest(app.dataModel.eNodebUrl, "GET", {
            name: viewModel.queryText()
        }, function (result) {
            for (var i = 0; i < result.length; i++) {
                addOneENodebMarker(result[i]);
            }
        }, function (result) {
            alert(getErrorMessage(result));
        });
    }
};

var queryBtss = function(viewModel) {
    removeAllBtss();
};