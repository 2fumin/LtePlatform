var updateDumpHistory = function (viewModel) {
    sendRequest(app.dataModel.preciseImportUrl, "GET", {
        begin: viewModel.beginDate(),
        end: viewModel.endDate()
    }, function (result) {
        viewModel.dumpHistory(result);
    });
};