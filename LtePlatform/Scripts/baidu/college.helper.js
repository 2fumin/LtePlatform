var addOneCollegeMarkerInfo = function (viewModel, data) {
    for (var i = 0; i < viewModel.collegeInfos().length; i++) {
        var info = viewModel.collegeInfos()[i];
        if (info.id == data.id) {
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

var removeAllColleges= function() {
    var count = map.CollegeMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.CollegeMarkers.pop());
    }
}