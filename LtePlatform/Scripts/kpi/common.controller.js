var initializeCityKpi = function(viewModel) {
    
    $.ajax({
        method: 'get',
        url: app.dataModel.cityListUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
        },
        success: function (data) {
            viewModel.cities(data);
            if (data.length > 0) {
                viewModel.currentCity(data[0]);
                viewModel.showKpi();
            }
        }
    });
};

var dumpProgressItems = function (viewModel, actionUrl) {
    sendRequest(actionUrl, "PUT", null, function (result) {
        if (result === true) {
            viewModel.totalSuccessItems(viewModel.totalSuccessItems() + 1);
        } else {
            viewModel.totalFailItems(viewModel.totalFailItems() + 1);
        }
        if (viewModel.totalSuccessItems() + viewModel.totalFailItems() < viewModel.totalDumpItems()) {
            viewModel.dumpItems();
        } else {
            viewModel.updateHistoryItems();
        }
    }, function () {
        viewModel.totalFailItems(viewModel.totalFailItems() + 1);
        if (viewModel.totalSuccessItems() + viewModel.totalFailItems() < viewModel.totalDumpItems()) {
            viewModel.dumpItems();
        } else {
            viewModel.updateHistoryItems();
        }
    });
};

var updateWorkItemTable = function(viewModel, items) {
    sendRequest(app.dataModel.workItemUrl, "GET", {
        statCondition: viewModel.currentState(),
        typeCondition: viewModel.currentType(),
        itemsPerPage: items
    }, function(result) {
        viewModel.totalPages(result);
        if (viewModel.currentPage() > result) {
            viewModel.currentPage(result);
        }
        viewModel.query();
    });
};