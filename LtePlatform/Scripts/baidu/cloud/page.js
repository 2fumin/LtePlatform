var map = null, markerList = [], defualtZoom = 15;

//历史数据模式
var historyMode = "";

$(function () {

    fixHeight();

    $(window).resize(function () {
        fixHeight();
    });

    loadCities();
    initHistoryTimes();
    initialAutoComplete('roadName', 'VALUE', 'TEXT');
    initColorBar();

    $('#city-name').click(function () {
        if ($('#city').css("display") == "none") {
            $('#city').show();
        }
        else {
            $('#city').hide();
        }
    });

    $('#txtKey').blur(function () {
        if ($.trim($(this).val()).length == 0) { $(this).val("输入关键字..."); }
    });

    $('#txtKey').focus(function () {
        if ($.trim($(this).val()) == "输入关键字...") { $(this).val(""); }
    });

    $('#wrapper').click(function (event) {
        var e = event || window.event;
        var elem = e.srcElement || e.target;

        if (elem.id == "city" || elem.parentNode.id == "city" || elem.parentNode.parentNode.id == "city" || elem.parentNode.id == "city-name") { return; }
        $('#city').hide();
    });

    var lastHour = new Date(new Date().getTime() - 60 * 60 * 1000);

    $('#dateHistory,#beginDate,#endDate').datetimebox({          //时间
        required: true,
        showSeconds: false,
        editable: false,
        formatter: function (date) {
            return getHistoryDate(date, $(this).attr("id"));
        },
        onChange: function (date) {
            if ($(this).attr("id") == "dateHistory") {
                changeHistory();
            }
        }

    });


    $('#dateHistory').datebox('setValue', getHistoryDate(lastHour, $(this).attr("id")));
    $('#beginDate').datebox('setValue', getHistoryDate(new Date(new Date().getTime() - 48 * 60 * 60 * 1000), 'beginDate'));
    $('#endDate').datebox('setValue', getHistoryDate(new Date(new Date().getTime() - 24 * 60 * 60 * 1000), 'beginDate'));


    var nScrollHight = 0; //滚动距离总长(注意不是滚动条的长度)   
    $('#bts').scroll(function () {
        nScrollHight = $(this)[0].scrollHeight;
        if ($(this)[0].scrollTop + $(this).height() == nScrollHight) {
            //$('#btsList').height($('#btsList').height() + 650);
            getBtsList();
        }
    });

    //选择数据类型
    $("#dataType").change(function () {
        initColorBar();
        if (TimeControl.playing) {
            TimeControl.reset();
        }

        reloadMap();
    });

    //匹配道路
    $("#btn_search").click(function () {
        if ($('#roadName').combobox('getText') == "输入关键字...") {
            alert("请输入道路名称");
            return;
        }
        else {
            GetRoadName();
        }
    });

    //判断全屏按钮文字
    var screenAction = getQueryString("screenAction");
    if (screenAction == "full") {
        // $('#aFullscreen').html('<img id="imgFullscreen" src="../../images/Mps/guanb.png" style="margin-top:1px; margin-right:2px;" />退出全屏');
        $('#icon_qp').hide();
        $('#icon_tcqp').show();
    }

    //取消对比
    $('#cancelDiff').click(function () {
        $('#time-control').show();
        $('#diff-select-control').hide();
        $('.icon_lsn').show();
        $('.icon_tcn').hide();
    });

    $('#beginMode,#endDate').change(function () {
        var dateBox = $(this).attr("id") == "beginMode" ? "beginDate" : "endDate";
        $('#' + dateBox).datebox('setText', getHistoryDate(getSelectDate($('#' + dateBox).datetimebox('getValue')), dateBox));
    });

    //透明度
    for (i = 10; i <= 100; i = i + 10) {
        $('#opacity').append('<option value="' + i + '">' + i + '%</option>');
    }
    $('#opacity').val(70);

    $('#opacity').change(function () {
        if (!TimeControl.playing) {
            reloadMap();
        }
    });
});



function fixHeight() {

    var height = getQueryString("h");

    if (height == null) {
        try {
            height = window.parent.document.documentElement.clientHeight - 120;
        }
        catch (e) {
            height = $(document).height();
        }
    }
    else {
        height = height;
    }

    $('#wrapper').css('height', height);
    $('#mapWrapper,#mapContent').css('height', height - $('#tools').height());
    try {
        resizeParentIframe();
    }
    catch (e) {

    }
}

//加载时间控件
function loadTimeControl() {
    TimeControl.setTimes(12, "day");
    TimeControl.showTimePanel();
    TimeControl.bindEvents();
    TimeControl.updateLayer = updateHeatMap;
}

function getSelectDate(date) {

    var arr = (date.split("-"));
    var y = parseInt(arr[0], 10);
    var m = parseInt(arr[1], 10);

    var d = 0;
    var h = 0;

    if (arr[2].length > 2) {
        d = arr[2].split(" ")[0];
        h = arr[2].split(" ")[1].split(":")[0];
    } else {
        d = arr[2];
    }


    return new Date(y, m - 1, d, h);

}

//选择模式
function selectMode(curDiv, mode) {

    $('#time-control-btns').children().removeClass('active');
    $('#' + curDiv).addClass('active');

    if (mode == "weeks") {
        TimeControl.setTimes(7, mode);
    }
    else {

        if (mode == "return") {
            $('#time-control-8hours').addClass('active');
            $('.map-k-time,#time-control-return').hide();
            $('#time-control-8hours,#time-control-week,#time-control-history').show();
            mode = "day";
            TimeControl.setTimes(12, mode);
            selectHistory('hour', false);
        }
        else if (mode == "history") {
            $('.map-k-time,#time-control-return').show();
            $('#time-control-return').addClass('active');
            $('#time-control-8hours,#time-control-week,#time-control-history').hide();

            historyMode = "hour";
            //var date = new Date(new Date().getTime() - 60 * 60 * 1000);
            var date = getSelectDate($('#dateHistory').datetimebox('getValue'));
            var time = date.getHours() < 0 ? 23 : date.getHours();

            TimeControl.setTimes(12, mode, time, date);

        }
        else
            TimeControl.setTimes(12, mode);
    }

    TimeControl.showTimePanel();

    reloadMap();
}

//选择历史天或小时
function selectHistory(type, changeData) {
    historyMode = type;

    if (type == "day") {
        $('#historyDay').removeClass("map-ztian-1").addClass("map-ztian-2");
        $('#historyTime').removeClass("map-ztian-2").addClass("map-ztian-1");
    } else {
        $('#historyDay').removeClass("map-ztian-2").addClass("map-ztian-1");
        $('#historyTime').removeClass("map-ztian-1").addClass("map-ztian-2");
    }

    if (changeData) {
        changeHistory();
    }
}

//格式化历史时间
function getHistoryDate(date, dateBoxId) {

    var mode = historyMode;
    if (dateBoxId) {
        if (dateBoxId == "beginDate") {
            mode = $('#beginMode').val();
        } else if (dateBoxId == "endDate") {
            mode = $('#endMode').val();
        }
    }

    var sy = date.getFullYear();
    var sm = date.getMonth() + 1;
    var sd = date.getDate();
    var stime = sy + '-' + (sm < 10 ? ('0' + sm) : sm) + '-' + (sd < 10 ? ('0' + sd) : sd);

    if (mode == "day") {
        return stime;
    } else {
        var hour = (date.getHours() < 10 ? ('0' + date.getHours().toString()) : date.getHours().toString());
        return stime + " " + hour + ":00";
    }

}

//选择历史触发查询
function changeHistory() {

    if (historyMode == "") { return; }

    var date = getSelectDate($('#dateHistory').datetimebox('getValue'));
    var times = $('#historyTimes').val();

    if (historyMode == "hour") {
        TimeControl.setTimes(times, "times", date.getHours(), date);
    }
    else {
        TimeControl.setTimes(times, "weeks", date.getDate(), date);
    }

    $('#dateHistory').datetimebox('setText', getHistoryDate(date));

    TimeControl.showTimePanel();

    reloadMap();

}

//初始化历史小时下拉控件
function initHistoryTimes() {
    for (var i = 12; i > 1; i--) {
        $('#historyTimes').append("<option>" + i + "</option>");
    }
}

//初始化右下角颜色区间
function initColorBar() {

    var type = $('#dataType').val();
    var arr = [];
    var isDiff = $('#diff-control').css("display") == "block";
    if (isDiff) {
        type += "差值";
    }

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=getThresholdConfig&t=" + new Date().getTime(),
        async: true,
        data: { dataType: type, pager: 0, zoom: map == null ? defualtZoom : map.getZoom() },
        success: function (data) {

            $('#colorBar').empty();
            var title = type;
            if (type == "RSRP" || type == "RSRQ") {
                title = 'MR覆盖情况(' + type + ')';
            }


            $('#colorBar').append(' <li class="map-w-1">' + title + '：</li>');
            $.each(data.rows, function (i, item) {
                $('#colorBar').append('<li class="map-w-i" style="background-color:' + item.COLOR + '; border:1px solid ' + item.COLOR + ';">' + item.TEXT + '：' + item.THRESHOLD + '</li>');
            });

        },
        error: function (msg) {
            alert("加载门限配置失败");
        }
    });
}

//加载城市
function loadCities() {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "CloudMap.ashx?action=getCities&t=" + new Date().getTime(),
        async: true,
        data: {},
        success: function (data) {
            $('#cities').empty();
            $.each(data, function (i, item) {
                $("<a href=\"javascript:selectCity('" + item.CITYCN + "');\">" + item.CITYCN + "</a>").appendTo($('#cities'));
            });
        },
        error: function (msg) {
            alert("查询数据失败");
        }
    });
}

//选择城市
function selectCity(city) {
    if (map) {
        $('#city-title,#lblCity').text(city + "市");
        map.setCenter(city);
        $('#city').hide();
        //getPoints();
        reloadMap();
    }
}


//获取道路数据
function GetRoadName() {
    ajaxLoading("查询中...");
    var queryParams = {};
    var thisKey = encodeURIComponent($('#roadName').combobox('getText')); //搜索词
    var thisCity = encodeURIComponent($('#lblCity').text());
    queryParams.city = thisCity;
    queryParams.key = thisKey;
    queryParams.type = "list";

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "CloudMap.ashx?action=GetAreas",
        async: true,
        data: queryParams,
        success: function (back) {
            //只有一条道路时
            if (back.length == 1) {
                if (!back[0]["SECTOR"]) {
                    alert("没有找到匹配的扇区...");
                }
                else {
                    $('#roadName').combobox("setValue", back[0]["SECTOR"]);
                    selectArea(back[0].LONGITUDE, back[0].LATITUDE);
                }
            }
                //多条道路时
            else if (back.length > 1) {
                $("#RoadLayer").show() //显示弹出层
                $('#RoadSelect').show(); //显示弹出层
                LoadRoadName(back);
            }
            else {
                alert("没有找到匹配的扇区...");
            }
            ajaxLoadEnd();
        },
        error: function (msg) {
            ajaxLoadEnd();
            if (msg.statusText != "abort") {
                alert("数据加载错误...");
            }
        }
    });
}

//道路呈现
function LoadRoadName(data) {
    var divItems = [];
    var div = $("<h3> 请选择扇区<a onclick=\"closeRoad();\">取消</a></h3>");
    divItems.push(div);
    var divData = $("<div class='scoll_div'></div>");
    var div = "";
    data.forEach(function (item) {
        div += "<p lng='" + item.LONGITUDE + "' lat='" + item.LATITUDE + "' ><a></a>" + item["SECTOR"] + "</p>";
    });
    divData.html(div);
    divItems.push(divData);
    $("#RoadSelect").html("")
    $("#RoadSelect").append(divItems);
    $(".scoll_div p").off("click").on({
        click: function () {
            selectArea(this.attributes["lng"].nodeValue, this.attributes["lat"].nodeValue);
            closeRoad();
        }
    });

    $("#RoadSelect").css("left", ($("body").width() - 300) / 2 + "px").css("z-index", 1005);
}

function closeRoad() {
    $('#RoadLayer').hide();
    $('#RoadSelect').hide();
}

//定位基站
function selectArea(lng, lat) {

    var p = new BMap.Point(lng, lat);
    map.centerAndZoom(p, 15);
    var marker = new BMap.Marker(p); // 创建点
    // marker.addEventListener('click', function (e) { map.removeOverlay(e.target); })

    $.each(markerList, function (i, item) {
        map.removeOverlay(item);
    })

    map.addOverlay(marker);
    markerList.push(marker);
    reloadMap();
}


//自动填充下拉菜单
var autoCompleteRequest;
function initialAutoComplete(vid, valueField, textField) {
    $('#' + vid).combobox({
        valueField: valueField, //'MDN', //TPrice
        textField: textField, //'MDN',
        //注册事件
        onSelect: function (item) {
            var value = item.VALUE;
            var arr = value.split('_');
            selectArea(arr[0], arr[1]);

        },
        onChange: function (newValue, oldValue) {
            if (autoCompleteRequest) {
                autoCompleteRequest.abort();
            }

            var queryParams = {};
            var thisKey = encodeURIComponent($('#' + vid).combobox('getText')); //搜索词
            var thisCity = encodeURIComponent($('#lblCity').text());
            queryParams.action = 'AUTOCOMPLETE';
            queryParams.city = thisCity;
            queryParams.key = thisKey;
            queryParams.type = "AutoComplete";

            autoCompleteRequest = $.ajax({
                type: "POST",
                dataType: "json",
                url: "CloudMap.ashx?action=GetAreas",
                async: true,
                data: queryParams,
                success: function (back) {
                    if (autoCompleteRequest) {
                        if (back.length > 1) {
                            $('#' + vid).combobox("loadData", back);
                        }
                    }
                    ajaxLoadEnd();
                },
                error: function (msg) {
                    ajaxLoadEnd();
                    if (msg.statusText != "abort" && msg.statusText != "OK") {
                        alert("数据加载错误");
                    }
                }
            });
        }
    });
};

//全屏
function fullscreen(flag) {
    if (flag == 1) {
        if (parent) {
            parent.showFullScreen("Modules/CloudMap/GridMap.html?screenAction=full");
            parent.document.body.style["overflow-y"] = "hidden";
        }
    } else {
        if (parent) {
            parent.document.body.style["overflow-y"] = "auto";
            parent.exitFullScreen();
        }
    }
}

//打开或者关闭对比面板
function showDiff(flag) {

    if (flag == 1) {
        $('.icon_tcn').show();
        $('.icon_lsn').hide();
        $('#time-control').hide();
        $('#diff-select-control').show();
    } else {
        $('.icon_lsn').show();
        $('.icon_tcn').hide();
        $('#time-control').show();
        $('#diff-select-control').hide();
        $('#diff-control').hide();

        initColorBar();
        reloadMap();
    }
}

//确定对比
function confirmDiff() {

    var beginDate = getSelectDate($('#beginDate').datetimebox('getValue'));
    var endDate = getSelectDate($('#endDate').datetimebox('getValue'));

    $('#lblBeginDate').text(getHistoryDate(beginDate, 'beginDate'));
    $('#lblEndDate').text(getHistoryDate(endDate, 'endDate'));

    if ($('#beginMode').val() == $('#endMode').val()) {
        if (endDate <= beginDate) { alert("结束时间必须大于开始时间"); return; }
    }

    $('#diff-select-control').hide();
    $('#diff-control').show();

    initColorBar();
    reloadMap();
}