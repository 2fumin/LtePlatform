var addOneCollegeMarkerInfo = function (viewModel, data) {
    for (var i = 0; i < viewModel.collegeInfos().length; i++) {
        var info = viewModel.collegeInfos()[i];
        if (info.id === data.id) {
            var marker = new BMap.Marker(new BMap.Point(info.centerx, info.centery));
            var html = generateCollegeInfoHtml(data);
            addOneMarker(marker, html, "College");
            break;
        }
    };
};

var generateCollegeInfoHtml = function(data) {
    $("#college-name").html(data.name);
    $("#college-subscribers").html(data.expectedSubscribers);
    $("#college-area").html(data.area.toFixed(2));
    $("#college-lte-enodebs").html(data.totalLteENodebs);
    $("#college-lte-cells").html(data.totalLteCells);
    $("#college-cdma-btss").html(data.totalCdmaBts);
    $("#college-cdma-cells").html(data.totalCdmaCells);
    $("#college-lte-distributions").html(data.totalLteIndoors);
    $("#college-cdma-distributions").html(data.totalCdmaIndoors);
    return $("#college-info-box").html();
};

var removeAllColleges = function() {
    var count = map.CollegeMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.CollegeMarkers.pop());
    }
};

var addOneLteDistributionMarkerInfo = function(data) {
    var eNodebIcon = new BMap.Icon("/Content/Images/Hotmap/site_or.png", new BMap.Size(20, 30));
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute), {
        icon: eNodebIcon
    });
    var html = generateDistributionInfoHtml(data);
    addOneMarker(marker, html, "LteDistribution");
};

var addOneCdmaDistributionMarkerInfo = function(data) {
    var btsIcon = new BMap.Icon("/Content/Images/Hotmap/site_bl.png", new BMap.Size(20, 30));
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute), {
        icon: btsIcon
    });
    var html = generateDistributionInfoHtml(data);
    addOneMarker(marker, html, "CdmaDistribution");
};

var generateDistributionInfoHtml = function(data) {
    $("#distribution-name").html(data.name);
    $("#distribution-range").html(data.range);
    $("#distribution-sourceName").html(data.sourceName);
    $("#distribution-sourceType").html(data.sourceType);
    $("#distribution-longtitute").html(data.longtitute);
    $("#distribution-lattitute").html(data.lattitute);
    return $("#distribution-info-box").html();
};