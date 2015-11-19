var initializeCityKpi = function() {
    // Make a call to the protected Web API by passing in a Bearer Authorization Header
    $.ajax({
        method: 'get',
        url: app.dataModel.cityListUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
        },
        success: function (data) {
            app.cities(data);
            if (data.length > 0) {
                app.currentCity(data[0]);
                app.showKpi();
            }
        }
    });
};