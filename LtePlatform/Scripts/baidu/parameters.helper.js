var addOneENodebMarker = function (data) {
    var eNodebIcon = new BMap.Icon("/Content/Images/Hotmap/site_or.png", new BMap.Size(20, 30));
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute), {
        icon: eNodebIcon
    });
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

var removeAllENodebs = function() {
    var count = map.eNodebMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.eNodebMarkers.pop());
    }
};

var addOneBtsMarker = function (data) {
    var btsIcon = new BMap.Icon("/Content/Images/Hotmap/site_bl.png", new BMap.Size(20, 30));
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute), {
         icon: btsIcon
    });
    var html = getBtsInfoHtml(data);
    addOneMarker(marker, html);
};

var getBtsInfoHtml= function(data) {
    $("#bts-id").html(data.btsId);
    $("#bts-name").html(data.name);
    $("#bts-address").html(data.address);
    $("#bts-bscid").html(data.bscId);
    $("#bts-longtitute").html(data.longtitute);
    $("#bts-lattitute").html(data.lattitute);
    return $("#bts-info-box").html();
}

var removeAllBtss = function() {
    var count = map.btsMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.btsMarkers.pop());
    }
};