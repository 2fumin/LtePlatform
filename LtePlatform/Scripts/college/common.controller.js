var initializeCollegeList = function (viewModel) {
    $.ajax({
        method: 'get',
        url: app.dataModel.collegeQueryUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
        },
        success: function (data) {
            viewModel.colleges.removeAll();
            for (var i = 0; i < data.length; i++) {
                viewModel.colleges.push(data[i].name);
            }
        }
    });
};