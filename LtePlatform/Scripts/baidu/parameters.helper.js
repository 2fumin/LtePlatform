var addOneENodebMarker = function (data) {
    var marker = new BMap.Marker(new BMap.Point(data.baiduLongtitute, data.baiduLattitute));
    var html = '<div class="infoBoxContent">'
        + '<div class="title"><strong>基站基本信息: </strong></div>'
        + '<div class="list"><ul>'
        + '<li><div class="left">ENodeb ID:</div><div class="rmb"> ' + data.eNodebId
        + '</div></li><li><div class="left">Name:</div><div class="rmb"> ' + data.name
        + '</div></li><li><div class="left">Address: </div><div class="rmb">' + data.address
        + '</div></li><li><div class="left">Factory: </div><div class="rmb">' + data.factory
        + '</div></li>'
        + '</ul></div>'
        + '</div>';
    addOneMarker(marker, html);
};

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