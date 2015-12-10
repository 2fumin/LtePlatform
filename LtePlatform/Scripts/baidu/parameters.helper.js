var addOneENodebMarker = function (data) {
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute));
    var html = getENodebInfoHtml(data);
    addOneMarker(marker, html, "ENodeb");
};

var getENodebInfoHtml = function(data) {
    $("#enodeb-id").html(data.eNodebId);
    $("#enodeb-name").html(data.name);
    $("#enodeb-address").html(data.address);
    $("#enodeb-factory").html(data.factory);
    $("#enodeb-planid").html(data.planNum);
    $("#enodeb-opendate").html(data.openDateString);
    $("#enodeb-longtitute").html(data.longtitute);
    $("#enodeb-lattitute").html(data.lattitute);
    return $("#enodeb-info-box").html();
};

var removeAllENodebs= function() {
    var count = map.eNodebMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.eNodebMarkers.pop());
    }
}

var addOneBtsMarker = function (data) {
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute));
    var html = '<div class="infoBoxContent">'
        + '<div class="title"><strong>基站基本信息: </strong></div>'
        + '<div class="list"><ul>'
        + '<li><div class="left">BTS ID:</div><div class="rmb"> ' + data.btsId
        + '</div></li><li><div class="left">Name:</div><div class="rmb"> ' + data.name
        + '</div></li><li><div class="left">Address: </div><div class="rmb">' + data.address
        + '</div></li><li><div class="left">BSC ID: </div><div class="rmb">' + data.bscId
        + '</div></li>'
        + '</ul></div>'
        + '</div>';
    addOneMarker(marker, html);
};