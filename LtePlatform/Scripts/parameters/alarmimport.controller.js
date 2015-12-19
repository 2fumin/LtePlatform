var updateDumpHistory = function (viewModel) {
    sendRequest(app.dataModel.dumpAlarmUrl, "GET", {
        begin: viewModel.beginDate(),
        end: viewModel.endDate()
    }, function (result) {
        viewModel.dumpHistory([]);
        viewModel.dumpHistory(result);
    });
};