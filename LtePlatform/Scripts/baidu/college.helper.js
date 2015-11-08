var addOneCollegeMarkerInfo = function (data) {
    var marker = new BMap.Marker(new BMap.Point(centerxs[data.id], centerys[data.id]));
    var html = '<div class="infoBoxContent">'
        + '<div class="title"><strong>校园基本信息: </strong></div>'
        + '<div class="list"><ul>'
        + '<li><div class="left">校园名称:</div><div class="rmb"> ' + data.name
        + '</div></li><li><div class="left">用户数:</div><div class="rmb"> ' + data.expectedSubscribers
        + '</div></li><li><div class="left">区域面积: </div><div class="rmb">' + data.area
        + '</div></li><li><div class="left">LTE基站数: </div><div class="rmb">' + data.totalLteENodebs
        + '</div></li><li><div class="left">LTE小区数: </div><div class="rmb">' + data.totalLteCells
        + '</div></li><li><div class="left">CDMA基站数: </div><div class="rmb">' + data.totalCdmaBts
        + '</div></li><li><div class="left">CDMA小区数: </div><div class="rmb">' + data.totalCdmaCells
        + '</div></li><li><div class="left">LTE室内分布数: </div><div class="rmb">' + data.totalLteIndoors
        + '</div></li><li><div class="left">CDMA室内分布数: </div><div class="rmb">' + data.totalCdmaIndoors
        + '</div></li>'
        + '</ul></div>'
        + '</div>';
    addOneMarker(marker, html);
};