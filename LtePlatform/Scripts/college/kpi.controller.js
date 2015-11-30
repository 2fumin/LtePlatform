var getKpiList = function (viewModel) {
    sendRequest(app.dataModel.collegeKpiUrl, "GET", {
         date: viewModel.date, hour: viewModel.hour
    }, function (data) {
        viewModel.kpis.removeAll();
        viewModel.kpis.push.apply(viewModel.kpis, data);
    });
};
