
$(function () {

    $('#dialogTip').dialog({
        buttons: [{
            text: '导出',
            iconCls: 'icon-print',
            handler: function () {
                exportTip();
            }
        }
        ]
    });

    $('#dialogTip').dialog("close");

    $('#dialogSector').dialog({
        buttons: [{
            text: '导出',
            iconCls: 'icon-print',
            handler: function () {
                exportSector();
            }
        }
        ]
    });

    $('#dialogSector').dialog("close");

    //先加载控件再加载地图
    loadTimeControl();
    TimeControl.onPause = onTimerPause;
    TimeControl.onPlay = onTimerPlay;

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=getUserCity&t=" + new Date().getTime(),
        async: true,
        data: {},
        success: function (data) {

            if (data.City == "Unkown") {
                alert("登录过期或者身份验证失败");
                return;
            }

            if (data.IsInterface == 1) {
                $('#fullscreen').hide();
            }

            loadMap(data.City);
        },
        error: function (msg) {
            loadMap();
        }
    });

    $('#markStation').click(function () {

        if (!isShowStationsZoom()) { alert("放大到500米才能打点"); return; }

        if (isShowStations) {
            clearStations();
            isShowStations = false;
            $(this).removeClass("icon_qxd").addClass("icon_jzd");
        } else {
            isShowStations = true;
            loadStations();
            $(this).removeClass("icon_jzd").addClass("icon_qxd");
        }
    });


});


var map = null, customLayer = null, queryResultExport = null, isAutoPlay = false;
layerList = [],
getGridTimer = null,//获取网格图定时器
isZoom = false,//是否真正缩放地图
currentPlayTime = null;//当前获取数据的时间

function loadMap(city) {

    map = new BMap.Map("mapContent", { enableMapClick: false });          // 创建地图实例

    var point = new BMap.Point(113.312213, 23.147267);//广州市中心
    map.centerAndZoom(point, 8);

    if (city != undefined && city.length > 0) {
        $('#lblCity,#city-title').text(city + '市');
        map.setCenter(city);
    }

    map.enableScrollWheelZoom(); // 允许滚轮缩放
    map.setDefaultCursor("url('bird.cur')");
    map.enableDragging();
    map.disableDoubleClickZoom();

    map.addEventListener('zoomstart', zoomStart);
    map.addEventListener('zoomend', zoomEnd);
    map.addEventListener('dragstart', dragStart);
    map.addEventListener('dragend', dragEnd);

    //map.addEventListener('mouseout', mouseout);
    map.addEventListener('mousemove', mousemove);
    map.addEventListener('click', clickMap);
    map.addEventListener("tilesloaded", loadLayer);

    //getGrids();
}


function mouseout() {
    $('#tip').hide();
}

//点击结果
function getTip(point) {

    $('#dialogTip').dialog("open");
    ajaxLoadingExt("tipContent", "", true); //打开等待
    $('#hfPositon').val(point.lng + "," + point.lat);

    var queryParam = getQueryParam();
    queryParam.lng = point.lng;
    queryParam.lat = point.lat;
    queryParam.isDetails = "1";

    var isDiff = queryParam.isDiff && queryParam.isDiff == 1;
    var type = decodeURIComponent(queryParam.type);


    var columns = new Array();
    var unit = "";
    if (type.indexOf("流量") > -1) {
        unit = "(MB)";
    }


    if (isDiff) {
        columns.push({ field: 'TIME1', title: '时间1', align: 'center', formatter: formatAutoSize });
        columns.push({ field: 'TIME2', title: '时间2', align: 'center', formatter: formatAutoSize });
    }

    columns.push({ field: 'LONGITUDE', title: '经度', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'LATITUDE', title: '纬度', align: 'center', formatter: formatAutoSize });

    if (isDiff) {
        columns.push({ field: 'VALUE1', title: type + '1' + unit, align: 'center', formatter: formatAutoSize });
        columns.push({ field: 'VALUE2', title: type + '2' + unit, align: 'center', formatter: formatAutoSize });
    }

    columns.push({ field: "VALUE", title: type + (isDiff ? "差值" : "") + unit, align: 'center', formatter: formatAutoSize });

    columns.push({ field: 'CITY', title: '地市', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'AREANAME', title: '片区', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'GRIDNAME', title: '网格', align: 'center', formatter: formatAutoSize });


    $('#tbTip').datagrid({
        columns: [columns],
        rownumbers: true
    });


    $('#tbTip').datagrid('loadData', { total: 0, rows: [] });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=GetPositionData&t=" + new Date().getTime(),
        async: true,
        data: queryParam,
        success: function (data) {
            ajaxLoadExtEnd("tipContent");
            $('#tbTip').datagrid('loadData', data);
        },
        error: function (msg) {
            ajaxLoadExtEnd("tipContent");
            alert("查询失败");
        }
    });
}

//取消导出数据
function cancelExport() {
    var yesOrNo = confirm("是否确定取消导出？");
    if (!yesOrNo) {
        return false;
    }

    if (queryResultExport) {
        queryResultExport.abort();
    }
    ajaxLoadEnd();
}

function exportTip() {

    var queryParam = getQueryParam();
    var point = $('#hfPositon').val().split(',');
    queryParam.lng = point[0];
    queryParam.lat = point[1];
    queryParam.isDetails = 1;


    ajaxLoadingExt("tipContent", "正在导出中……<a onclick='javascript:cancelExport();'>》取消《</a>", true);

    queryResultExport = $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=exportTip",
        async: true,
        data: queryParam,
        success: function (data) {
            ajaxLoadExtEnd("tipContent");
            window.open("../../upload/CloudMap/" + data.DownFilePath);
        },
        error: function (msg) {
            ajaxLoadExtEnd("tipContent");
            if (msg.statusText != 'abort') {
                if (msg.responseText.length > 0) {
                    alert(msg.responseText);
                }
                else
                    alert("导出失败");
            }
        }
    });

}

function clickMap(e) {
    $('#tip').hide();
    getTip(e.point);
}


//鼠标悬停
var lastMousemove = null;
var lastPos = null;

function mousemove(e) {
    var _self = this;
    var position = e.point;
    //var x = event.layerX;
    //var y = event.layerY;

    var x = e.offsetX;
    var y = e.offsetY;


    if (lastPos == null) {
        lastPos = position;
    }
    else if (lastPos.lng != position.lng && lastPos.lat != position.lat) {
        $('#tip').hide();
        lastPos = position;
        if (lastMousemove != null) { clearTimeout(lastMousemove); }
    }

    lastMousemove = setTimeout(function () {
        getPosData(position, x, y, e)
    }, 550);
}

var getPostionAjax = null;
function getPosData(position, x, y, e) {

    if (e.overlay && e.overlay.OverlayType != undefined) {

        var title = "";
        switch (e.overlay.OverlayType) {
            case "BTS":
                title = e.overlay.BTSNAME;
                break;
            case "SECTOR":
                title = e.overlay.SECTOR;
                break;
            case "PlanningBTS":
                title = e.overlay.BTSNAME;
                break;
        }

        if (title.length > 0) {
            $('#lblTip').text(title);
            $('#tip').css("left", x + 10 + "px").css("top", y + 12 + "px").show();
            return;
        }
    }

    var queryParam = getQueryParam(null);
    queryParam.lng = position.lng;
    queryParam.lat = position.lat;

    if (getPostionAjax != null && getPostionAjax.state() === 'pending') {
        getPostionAjax.abort();
    }

    getPostionAjax = $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=GetPositionData&t=" + new Date().getTime(),
        async: true,
        data: queryParam,
        success: function (data) {
            if (!data || data.length == 0) { return; }

            if (getPostionAjax.statusText != "abort") {
                var isDiff = queryParam.isDiff && queryParam.isDiff == 1;
                var typeTip = decodeURIComponent(queryParam.type) + (isDiff ? "差值" : "");
                $('#lblTip').text(typeTip + ":" + data[0].VALUE);
                $('#tip').css("left", x + 10 + "px").css("top", y + 12 + "px").show();
            }
        },
        error: function (msg) {
        }
    });

}

function dragStart(e) {
}

var lastZoom=null;
function zoomStart(e) {
    isZoom = true;
    lastZoom = map.getZoom();
}

function zoomEnd(e) {


    var zoom = map.getZoom();
    switch (zoom) {
        case 13:
        case 11:
        case 9:
            if (zoom > lastZoom) {
                map.setZoom(zoom + 1);
            } else {
                map.setZoom(zoom - 1);
            }
            return;
        default: break;
    }


    var type = $('#dataType').val();
    if (type == "用户云图") {
        initColorBar();
    }

    if (isShowStationsZoom()) {
        // $('#markStation').show();
        $('#markStation').attr("class", isShowStations ? "icon_qxd" : "icon_jzd").attr("title", "");
    } else {
        isShowStations = false;
        $('#markStation').attr("class", "icon_jzd2").attr("title", "500米内才能打点");
    }

    if (getGridTimer != null) {
        clearTimeout(getGridTimer);
    }

    getGridTimer = setTimeout(function () {
        isZoom = false;
        getGrids();
        loadStations();
    }, 500);
}

function dragEnd(e) {
    getGrids();
    loadStations();
}



//获取参数
function getQueryParam(opt) {

    var queryParam = {};
    queryParam.time = opt && opt.times != undefined ? opt.times : TimeControl.timeValue;
    queryParam.day = opt && opt.day != undefined ? opt.day : formatDate(TimeControl.currentDay);
    queryParam.mode = TimeControl.mode == "weeks" ? "DAY" : "HOUR";

    var bounds = map.getBounds();
    var sw = bounds.getSouthWest();
    var ne = bounds.getNorthEast();
    queryParam.minLng = sw.lng;
    queryParam.maxLng = ne.lng;
    queryParam.minLat = sw.lat;
    queryParam.maxLat = ne.lat;
    queryParam.zoom = map.getZoom();
    queryParam.type = encodeURIComponent($("#dataType").val());
    var center = map.getCenter();
    queryParam.centerLng = center.lng;
    queryParam.centerLat = center.lat;


    if ($('#diff-control').css("display") == "block") {
        queryParam.isDiff = 1;
        queryParam.beginMode = $('#beginMode').val();
        queryParam.endMode = $('#endMode').val();
        queryParam.beginDate = $('#lblBeginDate').text();
        queryParam.endDate = $('#lblEndDate').text();
    }

    return queryParam;
}

function onTimerPause() {
    isAutoPlay = false;
}

function onTimerPlay() {
    isAutoPlay = true;
}

function loadLayer(type, target) {

    if (layerList != null) {
        //var layer = layerList.pop();//取出第一个图层
        //if (layer != null) {
        //    map.removeTileLayer(layer);
        //}
        for (i = 0; i < layerList.length; i++) {
            map.removeTileLayer(layerList[i]);
        }
    }


    //if (isAutoPlay) {

    //    if (currentPlayTime == null) {
    //        currentPlayTime = TimeControl.currentTimes;
    //    }

    //    if (TimeControl.currentTimes == TimeControl.times) {
    //        currentPlayTime = null;
    //    }
    //    else {

    //        currentPlayTime += 1;
    //        if (!TimeControl.playing && currentPlayTime == TimeControl.currentTimes) {
    //            TimeControl.play();
    //        }
    //    }
    //}

}

function updateHeatMap(times) {

    //if (isAutoPlay && currentPlayTime != null) {
    //    if (currentPlayTime + 1 < TimeControl.currentTimes) {
    //        TimeControl.pause();
    //        return;
    //    }
    //}
    getGrids({ times: times });
}

//获取数据
function getGrids(opt) {
    var queryParam = getQueryParam(opt);
    $('#lblMode').text(queryParam.mode);
    $('#lblTimes').text(queryParam.day + " | " + queryParam.time);

    queryParam.height = $('#mapContent').height();
    queryParam.width = $('#mapContent').width();
    queryParam.opacity = $('#opacity').val();

    //ajaxLoading("云图加载中...")
    //$.getJSON('CloudMap.ashx?action=drawScreen', queryParam, function (data) {
    //    ajaxLoadExtEnd();
    //    AddIndex3GLayer(queryParam, false, data);
    //}).error(function () {
    //    ajaxLoadExtEnd();
    //});


    AddIndex3GLayer(queryParam, false, null);
}

//生成栅格图层
function AddIndex3GLayer(queryParams, isNext, xyList) {

    if (customLayer != null && !TimeControl.playing) {
        map.removeTileLayer(customLayer);
        //如果是重新播放，清除旧图层
        if (queryParams.time == TimeControl.getMinTime() && layerList != null) {
            $.each(layerList, function (i, item) {
                if (item != null) {
                    map.removeTileLayer(item);
                }
            });

            layerList = null;
        }
    }

    var i = 1;

    var coordList = [];

    var layer = new BMap.TileLayer({ isTransparentPng: true });
    layer.getTilesUrl = function (tileCoord, zoom) {

        if (isZoom) { return ""; }

        //var point = $.grep(xyList, function (item) {
        //    return item.x == tileCoord.x && item.y == tileCoord.y && zoom == item.level
        //});

        ////alert(i);
        ////i++;

        //if (point != null && point.length > 0) {
        //    return point[0].url;
        //} else {
        var x = tileCoord.x;
        var y = tileCoord.y;

        //对比参数
        var diffQuery = "";
        if (queryParams.isDiff) {
            diffQuery = "&isDiff=1&beginMode=" + queryParams.beginMode + "&endMode=" + queryParams.endMode + "&beginDate=" + encodeURIComponent(queryParams.beginDate) +
            "&endDate=" + encodeURIComponent(queryParams.endDate);
        }

        var g3url = "CloudMap.ashx?action=draw&mode=" + queryParams.mode + "&day=" + queryParams.day + "&time=" + queryParams.time + "&x=" + x + "&y=" + y + "&zoom=" + zoom
                     + "&type=" + (queryParams.type) + diffQuery + "&opacity=" + $('#opacity').val();
        return g3url;
        //}


    }


    map.addTileLayer(layer);
    if (customLayer != null && TimeControl.playing) {
        if (layerList == null) { layerList = new Array(); }
        layerList.push(customLayer);
    }
    customLayer = layer;
}



function reloadMap(opt) {

    if (customLayer != null) { map.removeTileLayer(customLayer); customLayer = null; }
    if (layerList != null) {
        $.each(layerList, function (i, item) {
            if (item != null) {
                map.removeTileLayer(item);
            }
        });
        layerList = null;
    }
    getGrids(opt);

}

//基站打点
var overlayList = [];
var isShowStations = false;

function isShowStationsZoom() {
    return map.getZoom() >= 15;
}

function loadStations() {

    map.clearOverlays();


    if (isShowStations && isShowStationsZoom()) {
        markStation();
    }
}

function getStationsTimeOut() {
    var zoom = map.getZoom();
    if (zoom === 14) { return 3000; }
    else if (zoom === 15) { return 500; }
    else { return 100; }
}

function markStation() {

    map.clearOverlays();

    var queryParam = getQueryParam();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=getStations&t=" + new Date().getTime(),
        async: true,
        data: queryParam,
        success: function (data) {
            if (!data) { return; }
            setTimeout(mark, getStationsTimeOut(), data);
        },
        error: function (msg) {
        }
    });
}

function mark(data) {

    var r = getRadius().rStation;
    var zoom = map.getZoom();

    if (data.Sataions) {

        $.each(data.Sataions, function (i, item) {
            var point = new BMap.Point(item.LONGITUDE, item.LATITUDE);
            var circle = new BMap.Circle(point, r, { strokeColor: "#d967fe", strokeWeight: 0.1, strokeOpacity: 0.3, fillColor: '#d967fe' }); //创建圆
            circle.BTSID = item.BTSID;
            circle.BTSNAME = item.BTSNAME;
            circle.OverlayType = "BTS";

            circle.addEventListener("click", sectorClick);
            map.addOverlay(circle);
            overlayList.push(circle);
        });
    }
    if (data.Sector) {
        $.each(data.Sector, function (i, item) {
            var point = new BMap.Point(item.LONGITUDE, item.LATITUDE);
            var polygon = new BMap.Polygon(add_sector(point, item.ANGLE, 65), {
                strokeColor: "#3269eb", strokeWeight: 2, strokeOpacity: 0.2, fillColor: '#3269eb', title: item.SECTOR
            });

            polygon.OverlayType = "SECTOR";
            polygon.BTSID = item.BTSID;
            polygon.CELLID = item.CELLID;
            polygon.BTSNAME = item.BTSNAME;
            polygon.SECTOR = item.SECTOR;
            polygon.addEventListener("click", sectorClick);
            map.addOverlay(polygon);
            overlayList.push(polygon);
        });
    }

    if (data.PlanStations) {
        var size = zoom < 15 ? 24 : 32;
        var icon = new BMap.Icon("../../images/Hotmap/bts_" + size + ".png", new BMap.Size(size, size));
        $.each(data.PlanStations, function (i, item) {
            var marker = new BMap.Marker(new BMap.Point(item.LONGITUDE, item.LATITUDE), { icon: icon });
            marker.OverlayType = "PlanningBTS";
            marker.BTSNAME = item.BTSNAME;
            marker.addEventListener("click", planningBTSClick);
            marker.LONGITUDE = item.LONGITUDE;
            marker.LATITUDE = item.LATITUDE;
            map.addOverlay(marker);
            overlayList.push(marker);
        });
    }

}

function planningBTSClick(e) {
    event.stopPropagation();

    var text = "经度：" + e.target.LONGITUDE + "<br/>";
    text += "纬度：" + e.target.LATITUDE + "<br/>";
    //  text += "基站名称:" + e.target.BTSNAME + "<br/>";
    text += "基站规划中......";

    var opts = {
        width: 200,     // 信息窗口宽度
        height: 100,     // 信息窗口高度
        title: "<b>" + e.target.BTSNAME + "</b>", // 信息窗口标题
        zindex: 2002,
    }

    var infoWindow = new BMap.InfoWindow(text, opts);  // 创建信息窗口对象 
    map.openInfoWindow(infoWindow, e.point); //开启信息窗口

}

function sectorClick(e, target, point, pixel) {

    event.stopPropagation();

    $('#dialogSector').dialog("open");
    ajaxLoadingExt("sectorContent", "", true); //打开等待
    $('#hfBTSID').val(e.target.BTSID);


    var queryParam = getQueryParam();
    queryParam.btsid = e.target.BTSID;
    queryParam.sector = e.target.CELLID;


    var columns = [
        { field: 'LONGITUDE', title: '经度', align: 'center', formatter: formatAutoSize },
        { field: 'LATITUDE', title: '纬度', align: 'center', formatter: formatAutoSize },
        //{ field: 'BTSNAME', title: '基站名称', align: 'center', formatter: formatAutoSize },
        { field: 'SECTOR', title: '扇区名称', align: 'center', formatter: formatAutoSize },
        { field: 'ISREALTIMEALARM', title: '实时告警', align: 'center', formatter: formatAutoSize },
        { field: 'ALARMNAME', title: '告警内容', align: 'center', formatter: formatAutoSize }
    ];



    $('#tbSector').datagrid({
        columns: [columns],
        rownumbers: true
    });


    $('#tbSector').datagrid('loadData', { total: 0, rows: [] });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=getSectorDetails&t=" + new Date().getTime(),
        async: true,
        data: queryParam,
        success: function (data) {
            ajaxLoadExtEnd("sectorContent");
            $('#tbSector').datagrid('loadData', data);
        },
        error: function (msg) {
            ajaxLoadExtEnd("sectorContent");
            alert("查询失败");
        }
    });
}


function exportSector() {

    var queryParam = getQueryParam();
    queryParam.btsid = $('#hfBTSID').val();
    queryParam.isDetails = 1;

    ajaxLoadingExt("sectorContent", "正在导出中……<a onclick='javascript:cancelExport();'>》取消《</a>", true);

    queryResultExport = $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=exportBTS",
        async: true,
        data: queryParam,
        success: function (data) {
            ajaxLoadExtEnd("sectorContent");
            window.open("../../upload/CloudMap/" + data.DownFilePath);
        },
        error: function (msg) {
            ajaxLoadExtEnd("sectorContent");
            if (msg.statusText != 'abort') {
                if (msg.length > 0) {
                    alert(msg);
                }
                else
                    alert("导出失败");
            }
        }
    });

}


