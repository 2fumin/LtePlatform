var initializeCityKpi = function(viewModel) {
    // Make a call to the protected Web API by passing in a Bearer Authorization Header
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