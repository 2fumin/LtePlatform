
var initializeCities = function(viewModel) {
    $.ajax({
        method: 'get',
        url: app.dataModel.cityListUrl,
        contentType: "application/json; charset=utf-8",
        success: function(data) {
            viewModel.cities(data);
            if (data.length > 0) {
                viewModel.currentCity(data[0]);
            }
        }
    });
};

var updateDistricts = function(viewModel, name) {
    sendRequest(app.dataModel.cityListUrl, "GET", {
        city: name
    }, function(data) {
        viewModel.districts(data);
        if (data.length > 0) {
            viewModel.currentDistrict(data[0]);
        }
    });
};

var updateTowns = function(viewModel, name) {
    sendRequest(app.dataModel.cityListUrl, "GET", {
        city: viewModel.currentCity(),
        district: name
    }, function(data) {
        viewModel.towns(data);
        if (data.length > 0) {
            viewModel.currentTown(data[0]);
        }
    });
};
