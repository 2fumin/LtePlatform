var map = { };

var initializeMap = function (tag, zoomLevel) {
    map = new BMap.Map(tag);
    map.centerAndZoom("佛山", zoomLevel);
    map.enableScrollWheelZoom(); //启用滚轮放大缩小
    var bdary = new BMap.Boundary();
    bdary.get("佛山市", function (rs) {       //获取行政区域
        var count = rs.boundaries.length; //行政区域的点有多少个
        if (count === 0) {
            alert('未能获取当前输入行政区域');
            return;
        }
        var pointArray = [];
        for (var i = 0; i < count; i++) {
            var ply = new BMap.Polygon(rs.boundaries[i], {
                strokeWeight: 2, strokeColor: "#ff0000", fillOpacity: 0.1
            }); //建立多边形覆盖物
            map.addOverlay(ply);  //添加覆盖物
            pointArray = pointArray.concat(ply.getPath());
        }
        map.setViewport(pointArray);    //调整视野                 
    });
}

var addOneMarker = function (marker, html) {
    map.addOverlay(marker);

    var infoBox = new BMapLib.InfoBox(map, html, {
        boxStyle: {
            background: "url('/Content/themes/baidu/tipbox.jpg') no-repeat center top",
            width: "270px",
            height: "200px"
        },
        closeIconUrl: "/Content/themes/baidu/close.png",
        closeIconMargin: "1px 1px 0 0",
        enableAutoPan: true,
        align: INFOBOX_AT_TOP
    });
    marker.addEventListener("click", function () {
        
        infoBox.open(this);
    });
};