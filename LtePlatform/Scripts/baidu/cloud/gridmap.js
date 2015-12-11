
$(function () {

    $('#dialogTip').dialog({
        buttons: [{
            text: '����',
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
            text: '����',
            iconCls: 'icon-print',
            handler: function () {
                exportSector();
            }
        }
        ]
    });

    $('#dialogSector').dialog("close");

    //�ȼ��ؿؼ��ټ��ص�ͼ
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
                alert("��¼���ڻ��������֤ʧ��");
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

        if (!isShowStationsZoom()) { alert("�Ŵ�500�ײ��ܴ��"); return; }

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
getGridTimer = null,//��ȡ����ͼ��ʱ��
isZoom = false,//�Ƿ��������ŵ�ͼ
currentPlayTime = null;//��ǰ��ȡ���ݵ�ʱ��

function loadMap(city) {

    map = new BMap.Map("mapContent", { enableMapClick: false });          // ������ͼʵ��

    var point = new BMap.Point(113.312213, 23.147267);//����������
    map.centerAndZoom(point, 8);

    if (city != undefined && city.length > 0) {
        $('#lblCity,#city-title').text(city + '��');
        map.setCenter(city);
    }

    map.enableScrollWheelZoom(); // �����������
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

//������
function getTip(point) {

    $('#dialogTip').dialog("open");
    ajaxLoadingExt("tipContent", "", true); //�򿪵ȴ�
    $('#hfPositon').val(point.lng + "," + point.lat);

    var queryParam = getQueryParam();
    queryParam.lng = point.lng;
    queryParam.lat = point.lat;
    queryParam.isDetails = "1";

    var isDiff = queryParam.isDiff && queryParam.isDiff == 1;
    var type = decodeURIComponent(queryParam.type);


    var columns = new Array();
    var unit = "";
    if (type.indexOf("����") > -1) {
        unit = "(MB)";
    }


    if (isDiff) {
        columns.push({ field: 'TIME1', title: 'ʱ��1', align: 'center', formatter: formatAutoSize });
        columns.push({ field: 'TIME2', title: 'ʱ��2', align: 'center', formatter: formatAutoSize });
    }

    columns.push({ field: 'LONGITUDE', title: '����', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'LATITUDE', title: 'γ��', align: 'center', formatter: formatAutoSize });

    if (isDiff) {
        columns.push({ field: 'VALUE1', title: type + '1' + unit, align: 'center', formatter: formatAutoSize });
        columns.push({ field: 'VALUE2', title: type + '2' + unit, align: 'center', formatter: formatAutoSize });
    }

    columns.push({ field: "VALUE", title: type + (isDiff ? "��ֵ" : "") + unit, align: 'center', formatter: formatAutoSize });

    columns.push({ field: 'CITY', title: '����', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'AREANAME', title: 'Ƭ��', align: 'center', formatter: formatAutoSize });
    columns.push({ field: 'GRIDNAME', title: '����', align: 'center', formatter: formatAutoSize });


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
            alert("��ѯʧ��");
        }
    });
}

//ȡ����������
function cancelExport() {
    var yesOrNo = confirm("�Ƿ�ȷ��ȡ��������");
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


    ajaxLoadingExt("tipContent", "���ڵ����С���<a onclick='javascript:cancelExport();'>��ȡ����</a>", true);

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
                    alert("����ʧ��");
            }
        }
    });

}

function clickMap(e) {
    $('#tip').hide();
    getTip(e.point);
}


//�����ͣ
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
                var typeTip = decodeURIComponent(queryParam.type) + (isDiff ? "��ֵ" : "");
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
    if (type == "�û���ͼ") {
        initColorBar();
    }

    if (isShowStationsZoom()) {
        // $('#markStation').show();
        $('#markStation').attr("class", isShowStations ? "icon_qxd" : "icon_jzd").attr("title", "");
    } else {
        isShowStations = false;
        $('#markStation').attr("class", "icon_jzd2").attr("title", "500���ڲ��ܴ��");
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



//��ȡ����
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
        //var layer = layerList.pop();//ȡ����һ��ͼ��
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

//��ȡ����
function getGrids(opt) {
    var queryParam = getQueryParam(opt);
    $('#lblMode').text(queryParam.mode);
    $('#lblTimes').text(queryParam.day + " | " + queryParam.time);

    queryParam.height = $('#mapContent').height();
    queryParam.width = $('#mapContent').width();
    queryParam.opacity = $('#opacity').val();

    //ajaxLoading("��ͼ������...")
    //$.getJSON('CloudMap.ashx?action=drawScreen', queryParam, function (data) {
    //    ajaxLoadExtEnd();
    //    AddIndex3GLayer(queryParam, false, data);
    //}).error(function () {
    //    ajaxLoadExtEnd();
    //});


    AddIndex3GLayer(queryParam, false, null);
}

//����դ��ͼ��
function AddIndex3GLayer(queryParams, isNext, xyList) {

    if (customLayer != null && !TimeControl.playing) {
        map.removeTileLayer(customLayer);
        //��������²��ţ������ͼ��
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

        //�ԱȲ���
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

//��վ���
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
            var circle = new BMap.Circle(point, r, { strokeColor: "#d967fe", strokeWeight: 0.1, strokeOpacity: 0.3, fillColor: '#d967fe' }); //����Բ
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
            alert("��ѯʧ��");
        }
    });
    var TimeControl = {
        playing: false, currentTimes: 0, times: 8
           , progress: 0, timeoutID: 0, interval: 70, pace: 0
           , $progress: $("html"), $el: $("html"), $btn: $("html")
           , mode: "" //ģʽ��day��weeks��history
           , maxTime: 0 //���̶�
           , maxDay: null //�������
           , currentDay: null //��ǰ�̶ȶ�Ӧ����
           , timeList: []
           , timeValue: 0
    };

    TimeControl.getMinTime = function () {
        return this.timeList[0];
    }

    TimeControl.setTimes = function (times, mode, time, day) {


        var isWeeks = mode && mode == "weeks";
        if (isWeeks) {
            this.maxDay = day ? day : new Date(new Date().getTime() - 24 * 60 * 60 * 1000);
            time = time ? time : this.maxDay.getDay();
        } else {
            time = time ? time : new Date().getHours() - 2;
            this.maxDay = day ? day : new Date(new Date().getTime() - 2 * 60 * 60 * 1000);
        }

        this.times = times;
        this.mode = mode;
        this.maxTime = time;
    }

    TimeControl.getTimeBarWidth = function () {
        return this.times * this.pace - 38;
    }

    TimeControl.show = function () {

        $('#progressText').empty();
        var isWeeks = this.mode == "weeks";
        $('#time-panel').css('width', this.times * this.pace);
        this.$el.find('.time-panel-control').css('width', this.times * this.pace);
        $('.time-panel-progress-container').css('width', this.getTimeBarWidth());

        this.timeList = new Array();

        for (i = 0; i < this.times; i++) {

            var timeName = "";
            if (isWeeks) {
                var day = new Date(this.maxDay.getTime() + ((1 - (this.times - i)) * 24 * 60 * 60 * 1000));
                //var day = this.maxDay;
                timeName = (day.getMonth() + 1) + "." + day.getDate();
                this.timeList.push(i == 0 ? 7 : i);
            }
            else {
                var time = this.maxTime - this.times + i + 1;
                time = (time < 0 ? 24 + time : time);

                timeName = time.toString();

                this.timeList.push(time);
            }
            $('#progressText').append('<span class=\"time-panel-progress-tick\" onclick=\"selectTick(' + i + ')\">' + timeName + '</span>');

        }

        this.$el.find('.time-panel-progress-tick').css('width', (100 / this.times) + '%');

        var t = this.maxDay; i = t.getFullYear();
        a = t.getMonth() + 1; s = t.getDate(); n = t.getHours();
        r = t.getMinutes(); n = 10 > n ? "0" + n : n; r = 10 > r ? "0" + r : r;

        this.$el.find(".time-panel-date").text(i + "." + a + "." + s);

        if (isWeeks) {
            this.$el.find(".time-panel-time").empty();
        }
        else {
            this.$el.find(".time-panel-time").text(n + ":" + r);
        }

        this.intTime();

    };
    TimeControl.setPace = function (e, t) {
        t = parseInt(t);
        this.pace = t || this.pace;
        e = parseInt(e);
        this.times = Math.ceil(e / t / 45);
        this.backupTimes = this.times;
        this.backupPace = this.pace;
    }
    TimeControl.bindEvents = function () {
        var e = this;
        this.$btn.bind("click", function () {
            if (e.playing) {
                e.onPause();
                e.pause();
            } else {
                e.onPlay();
                e.play();
            }
        });
    };
    TimeControl.pause = function () {
        clearTimeout(this.timeoutID);
        this.playing = false;
        this.$btn.addClass("play");
    };
    TimeControl.reset = function () {
        this.playing = false;
        this.currentTimes = 0;
        this.progress = 0;
        this.$progress.css("left", "0px");
        this.pause();
    };

    TimeControl.intTime = function (time) {
        //���õ�ǰ����Ϊ�̶ȿ�ʼ����

        time = time != undefined ? time : 0;

        if (this.mode == "weeks") {
            this.currentDay = new Date(this.maxDay.getTime() + ((time - this.times + 1) * 24 * 60 * 60 * 1000));
        } else {
            this.currentDay = new Date(this.maxDay.getTime() + ((time - this.times + 1) * 60 * 60 * 1000));
        }

        this.currentTimes = time;
        this.timeValue = this.timeList[this.currentTimes];
    }

    TimeControl.play = function () {
        var e = this;
        this.playing = true;
        this.$btn.removeClass("play");

        if (this.currentTimes >= this.times - 1 || this.currentTimes == 0) {
            this.progress = 0;
            this.intTime();
            this.$progress.css("left", "0px");
        }
        this.timeoutID = setTimeout(function () { e._tick() }, this.interval);

    };
    TimeControl._tick = function () {
        var e = this;
        var t = this.getTimeBarWidth();
        var speed = 1000;
        var end = speed * (1 - (this.times > 5 ? 0.8 : 0.6) / this.times);

        if (this.progress < end) {
            //this.progress++;

            this.progress += 12 / this.times;

            if (this.progress / 10 > this.currentTimes / this.times * 100) {

                this.timeValue = this.timeList[this.currentTimes];
                this.updateLayer(this.timeValue);
                this.currentTimes++;

                //��ǰ�����ۼ�1���̶ȵ�ʱ��
                if (this.mode == "weeks") {
                    this.currentDay = new Date(this.currentDay.setDate(this.currentDay.getDate() + 1));//��1��
                } else {
                    this.currentDay = new Date(this.currentDay.getTime() + 60 * 60 * 1000);//��1Сʱ
                }
            }

            this.$progress.css("left", this.progress / speed * t + "px");
            this.timeoutID = setTimeout(function () { e._tick(); }, this.interval);
        }
        else {
            this.pause();
        }
    };
    TimeControl.showTimePanel = function () {

        this.$el = $("#time-panel");
        this.$progress = $("#time-panel .time-panel-progress");
        this.$btn = $("#time-panel .time-panel-btn");

        this.reset();

        this.pace = this.times < 8 ? 250 / this.times : 33;
        this.show();

        var span = this.getTimeBarWidth() / this.times;
        var current = 0.5 * span - 8;
        this.$progress.css("left", current + "px");
    };
    TimeControl.updateLayer = function (currentTimes) {

        //alert(currentTimes);
    };

    TimeControl.onPlay = function () {

    }

    TimeControl.onPause = function () {

    }

    function selectTick(time) {
        TimeControl.currentTimes = time;
        var span = TimeControl.getTimeBarWidth() / TimeControl.times;
        TimeControl.$progress.css("left", ((time + 0.5) * span - 8) + "px");
        TimeControl.progress = ((time + (time < 5 ? 0.4 : 0.25)) / TimeControl.times) * 1000;//������������λ��


        TimeControl.intTime(time);
        TimeControl.updateLayer(TimeControl.timeList[time]);
    }
