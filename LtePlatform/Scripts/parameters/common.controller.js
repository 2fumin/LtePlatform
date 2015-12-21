var isLongtituteValid = function (longtitute) {
    return (!isNaN(longtitute)) && longtitute > 112 && longtitute < 114;
};

var isLattituteValid = function (lattitute) {
    return (!isNaN(lattitute)) && lattitute > 22 && lattitute < 24;
};

var isLonLatValid = function (item) {
    return isLongtituteValid(item.longtitute) && isLattituteValid(item.lattitute);
};

var mapLonLat = function (source, destination) {
    source.longtitute = destination.longtitute;
    source.lattitute = destination.lattitute;
};

var mapLonLatEdits = function (sourceFunc, destList) {
    var sourceList = sourceFunc();
    for (var i = 0; i < destList.length; i++) {
        destList[i].longtitute = parseFloat(destList[i].longtitute);
        destList[i].lattitute = parseFloat(destList[i].lattitute);
        if (isLonLatValid(destList[i])) {
            console.log(destList[i]);
            mapLonLat(sourceList[destList[i].index], destList[i]);
        }
    }
    sourceFunc(sourceList);
};

var initializeCities = function(viewModel) {
    $.ajax({
        method: 'get',
        url: app.dataModel.cityListUrl,
        contentType: "application/json; charset=utf-8",
        headers: {
            'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
        },
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
