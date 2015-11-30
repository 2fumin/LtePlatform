var updateSectorSelection = function (name, viewModel) {
    sendRequest(app.dataModel.cellUrl, "GET", { eNodebName: name }, function (data) {
        viewModel.sectorSelection.removeAll();
        viewModel.sectorSelection.push.apply(viewModel.sectorSelection, data);
    });
};

var getTestList = function (viewModel) {
    update3GList(viewModel);
    update4GList(viewModel);
};

var update3GList = function (viewModel) {
    sendRequest(app.dataModel.college3GTestUrl, "GET", {
        date: viewModel.date,
        hour: viewModel.hour
    }, function (data) {
        viewModel.test3G.removeAll();
        viewModel.test3G.push.apply(viewModel.test3G, data);
    });
};

var update4GList = function (viewModel) {
    sendRequest(app.dataModel.college4GTestUrl, "GET", {
        date: viewModel.date,
        hour: viewModel.hour
    }, function (data) {
        viewModel.test4G.removeAll();
        viewModel.test4G.push.apply(viewModel.test4G, data);
    });
};
