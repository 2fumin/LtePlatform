var map = { };

var initializeMap = function (tag, zoomLevel) {
    map = new BMap.Map(tag);
    map.centerAndZoom("佛山", zoomLevel);
    map.setMinZoom(8);  //设置地图最小级别
    map.setMaxZoom(17);  //设置地图最大级别

    map.enableScrollWheelZoom(); //启用滚轮放大缩小
    map.enableDragging();
    map.disableDoubleClickZoom();

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

    var topLeftControl = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT });// 左上角，添加比例尺
    var topLeftNavigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件

    map.addControl(topLeftControl);
    map.addControl(topLeftNavigation);

    map.collegeMarkers = [];
    map.eNodebMarkers = [];
    map.btsMarkers = [];
    map.lteSectors = [];
    map.cdmaSectors = [];
}

var addOneMarker = function (marker, html, type) {
    if (type === undefined) type = "College";
    switch (type) {
        case "ENodeb":
            map.eNodebMarkers.push(marker);
            break;
        case "Bts":
            map.btsMarkers.push(marker);
        default:
            map.collegeMarkers.push(marker);
            break;
    }
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

var addOneSector = function(sector, html, type) {
    if (type === undefined) type = "LteCell";
    switch (type) {
    case "CdmaCell":
        map.cdmaSectors.push(sector);
        break;
    default:
        map.lteSectors.push(sector);
        break;
    }
    map.addOverlay(sector);

    var infoBox = new BMapLib.InfoBox(map, html, {
        boxStyle: {
            background: "url('/Content/themes/baidu/tipbox.jpg') no-repeat center top",
            width: "270px",
            height: "200px"
        },
        closeIconUrl: "/Content/themes/baidu/close.png",
        closeIconMargin: "1px 1px 0 0",
        enableAutoPan: false,
        align: INFOBOX_AT_TOP
    });
    sector.addEventListener("click", function() {
        infoBox.open(this);
    });
};
