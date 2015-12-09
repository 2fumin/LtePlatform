var addOneCollegeMarkerInfo = function (data) {
    var marker = new BMap.Marker(new BMap.Point(centerxs[data.id], centerys[data.id]));
    var html = generateCollegeInfoHtml(data);
    addOneMarker(marker, html);
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