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

var getBtsInfoHtml = function(data) {
    $("#bts-id").html(data.btsId);
    $("#bts-name").html(data.name);
    $("#bts-address").html(data.address);
    $("#bts-bscid").html(data.bscId);
    $("#bts-longtitute").html(data.longtitute);
    $("#bts-lattitute").html(data.lattitute);
    return $("#bts-info-box").html();
};

var removeAllBtss = function() {
    var count = map.btsMarkers.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.btsMarkers.pop());
    }
};

var addOneGeneralSector = function(data, type) {
    var center = { lng: data.baiduLongtitute, lat: data.baiduLattitute };
    var iangle = 65;
    var irotation = data.azimuth - iangle / 2;
    var zoom = map.getZoom();
    var points = generateSectorPolygonPoints(center, irotation, iangle, zoom);
    
    var sector = new BMap.Polygon(points, {
        strokeWeight: 2,
        strokeColor: "blue",
        fillColor: "blue",
        fillOpacity: 0.5
    });
    var html = getSectorInfoHtml(data);
    addOneSector(sector, html, type);
};

var getSectorInfoHtml = function (data) {
    $("#sector-cellname").html(data.cellName);
    $("#sector-indoor").html(data.indoor);
    $("#sector-azimuth").html(data.azimuth);
    $("#sector-height").html(data.height);
    $("#sector-downtilt").html(data.downTilt);
    $("#sector-antennagain").html(data.antennaGain);
    $("#sector-frequency").html(data.frequency);
    $("#sector-otherinfo").html(data.otherInfo);
    return $("#sector-info-box").html();
};

var removeAllLteSectors = function() {
    var count = map.lteSectors.length;
    for (var i = 0; i < count; i++) {
        map.removeOverlay(map.lteSectors.pop());
    }
};