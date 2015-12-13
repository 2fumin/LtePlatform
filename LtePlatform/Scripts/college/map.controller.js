
var drawCollegeRegions = function (viewModel, result) {
    var type = result.regionType;
    var info = result.info;
    var coors = info.split(';');
    var centerx = 0;
    var centery = 0;
    if (type === 2) {
        var points = [];
        for (var p = 0; p < coors.length / 2; p++) {
            points.push(new BMap.Point(parseFloat(coors[2 * p]), parseFloat(coors[2 * p + 1])));
            centerx += parseFloat(coors[2 * p]);
            centery += parseFloat(coors[2 * p + 1]);
        }
        centerx /= coors.length / 2;
        centery /= coors.length / 2;
        var polygon = new BMap.Polygon(points,
            { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(polygon);
    } else if (type === 1) {
        centerx = (parseFloat(coors[0]) + parseFloat(coors[2])) / 2;
        centery = (parseFloat(coors[1]) + parseFloat(coors[3])) / 2;
        var rectangle = new BMap.Polygon([
            new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
            new BMap.Point(parseFloat(coors[2]), parseFloat(coors[1])),
            new BMap.Point(parseFloat(coors[2]), parseFloat(coors[3])),
            new BMap.Point(parseFloat(coors[0]), parseFloat(coors[3]))
        ], { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(rectangle);
    } else if (type === 0) {
        centerx = parseFloat(coors[0]);
        centery = parseFloat(coors[1]);
        var circle = new BMap.Circle(new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
            parseFloat(coors[2]),
            { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
        map.addOverlay(circle);
    }

    for (var i = 0; i < viewModel.collegeInfos().length; i++) {
        info = viewModel.collegeInfos()[i];
        if (info.id === result.areaId) {
            info.centerx = centerx;
            info.centery = centery;
            var opts = {
                position: new BMap.Point(centerx, centery),    // 指定文本标注所在的地理位置
                offset: new BMap.Size(10, -20)    //设置文本偏移量
            };
            var label = new BMap.Label(info.name, opts);  // 创建文本标注对象
            label.setStyle({
                color: "red",
                fontSize: "12px",
                height: "20px",
                lineHeight: "20px",
                fontFamily: "微软雅黑"
            });
            map.addOverlay(label);
            break;
        }
    }

};

var drawCollegeMap = function (viewModel, data) {
    for (var index = 0; index < data.length; index++) {
        var info = {};
        info.id = data[index].id;
        info.name = data[index].name;
        viewModel.collegeInfos.push(info);
        $.ajax({
            method: 'get',
            url: app.dataModel.collegeRegionUrl + '/' + info.id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (result) {
                drawCollegeRegions(viewModel, result);
            }
        });
        $.ajax({
            method: 'get',
            url: app.dataModel.collegeStatUrl + '/' + info.id,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: function (college) {
                addOneCollegeMarkerInfo(viewModel, college);
            }
        });
    }
};

var drawCollegeENodebs = function(viewModel) {
    if (map.eNodebMarkers.length > 0) return;
    sendRequest(app.dataModel.collegeENodebUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneENodebMarker(result[i]);
        }
    });
};

var drawCollegeBtss = function(viewModel) {
    if (map.btsMarkers.length > 0) return;
    sendRequest(app.dataModel.collegeBtssUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneBtsMarker(result[i]);
        }
    });
};

var drawCollegeCells = function(viewModel) {
    if (map.lteSectors.length > 0) return;
    sendRequest(app.dataModel.collegeCellsUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneGeneralSector(result[i], "LteCell");
        }
    });
};

var drawCollegeCdmaCells = function(viewModel) {
    if (map.cdmaSectors.length > 0) return;
    sendRequest(app.dataModel.collegeCdmaCellsUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneGeneralSector(result[i], "CdmaCell");
        }
    });
};

var drawCollegeLteDistributions = function(viewModel) {
    if (map.lteDistributions.length > 0) return;
    sendRequest(app.dataModel.collegeLteDistributionsUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneLteDistributionMarkerInfo(result[i]);
        }
    });
};

var drawCollegeCdmaDistributions = function(viewModel) {
    if (map.cdmaDistributions.length > 0) return;
    sendRequest(app.dataModel.collegeCdmaDistributionsUrl, "POST", viewModel.collegeNames(), function(result) {
        for (var i = 0; i < result.length; i++) {
            addOneCdmaDistributionMarkerInfo(result[i]);
        }
    });
};
