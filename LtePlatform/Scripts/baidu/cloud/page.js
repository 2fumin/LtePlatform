var map = null, markerList = [], defualtZoom = 15;

//��ʷ����ģʽ
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
        if ($.trim($(this).val()).length == 0) { $(this).val("����ؼ���..."); }
    });

    $('#txtKey').focus(function () {
        if ($.trim($(this).val()) == "����ؼ���...") { $(this).val(""); }
    });

    $('#wrapper').click(function (event) {
        var e = event || window.event;
        var elem = e.srcElement || e.target;

        if (elem.id == "city" || elem.parentNode.id == "city" || elem.parentNode.parentNode.id == "city" || elem.parentNode.id == "city-name") { return; }
        $('#city').hide();
    });

    var lastHour = new Date(new Date().getTime() - 60 * 60 * 1000);

    $('#dateHistory,#beginDate,#endDate').datetimebox({          //ʱ��
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


    var nScrollHight = 0; //���������ܳ�(ע�ⲻ�ǹ������ĳ���)   
    $('#bts').scroll(function () {
        nScrollHight = $(this)[0].scrollHeight;
        if ($(this)[0].scrollTop + $(this).height() == nScrollHight) {
            //$('#btsList').height($('#btsList').height() + 650);
            getBtsList();
        }
    });

    //ѡ����������
    $("#dataType").change(function () {
        initColorBar();
        if (TimeControl.playing) {
            TimeControl.reset();
        }

        reloadMap();
    });

    //ƥ���·
    $("#btn_search").click(function () {
        if ($('#roadName').combobox('getText') == "����ؼ���...") {
            alert("�������·����");
            return;
        }
        else {
            GetRoadName();
        }
    });

    //�ж�ȫ����ť����
    var screenAction = getQueryString("screenAction");
    if (screenAction == "full") {
        // $('#aFullscreen').html('<img id="imgFullscreen" src="../../images/Mps/guanb.png" style="margin-top:1px; margin-right:2px;" />�˳�ȫ��');
        $('#icon_qp').hide();
        $('#icon_tcqp').show();
    }

    //ȡ���Ա�
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

    //͸����
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

//����ʱ��ؼ�
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

//ѡ��ģʽ
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

//ѡ����ʷ���Сʱ
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

//��ʽ����ʷʱ��
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

//ѡ����ʷ������ѯ
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

//��ʼ����ʷСʱ�����ؼ�
function initHistoryTimes() {
    for (var i = 12; i > 1; i--) {
        $('#historyTimes').append("<option>" + i + "</option>");
    }
}

//��ʼ�����½���ɫ����
function initColorBar() {

    var type = $('#dataType').val();
    var arr = [];
    var isDiff = $('#diff-control').css("display") == "block";
    if (isDiff) {
        type += "��ֵ";
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
                title = 'MR�������(' + type + ')';
            }


            $('#colorBar').append(' <li class="map-w-1">' + title + '��</li>');
            $.each(data.rows, function (i, item) {
                $('#colorBar').append('<li class="map-w-i" style="background-color:' + item.COLOR + '; border:1px solid ' + item.COLOR + ';">' + item.TEXT + '��' + item.THRESHOLD + '</li>');
            });

        },
        error: function (msg) {
            alert("������������ʧ��");
        }
    });
}

//���س���
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
            alert("��ѯ����ʧ��");
        }
    });
}

//ѡ�����
function selectCity(city) {
    if (map) {
        $('#city-title,#lblCity').text(city + "��");
        map.setCenter(city);
        $('#city').hide();
        //getPoints();
        reloadMap();
    }
}


//��ȡ��·����
function GetRoadName() {
    ajaxLoading("��ѯ��...");
    var queryParams = {};
    var thisKey = encodeURIComponent($('#roadName').combobox('getText')); //������
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
            //ֻ��һ����·ʱ
            if (back.length == 1) {
                if (!back[0]["SECTOR"]) {
                    alert("û���ҵ�ƥ�������...");
                }
                else {
                    $('#roadName').combobox("setValue", back[0]["SECTOR"]);
                    selectArea(back[0].LONGITUDE, back[0].LATITUDE);
                }
            }
                //������·ʱ
            else if (back.length > 1) {
                $("#RoadLayer").show() //��ʾ������
                $('#RoadSelect').show(); //��ʾ������
                LoadRoadName(back);
            }
            else {
                alert("û���ҵ�ƥ�������...");
            }
            ajaxLoadEnd();
        },
        error: function (msg) {
            ajaxLoadEnd();
            if (msg.statusText != "abort") {
                alert("���ݼ��ش���...");
            }
        }
    });
}

//��·����
function LoadRoadName(data) {
    var divItems = [];
    var div = $("<h3> ��ѡ������<a onclick=\"closeRoad();\">ȡ��</a></h3>");
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

//��λ��վ
function selectArea(lng, lat) {

    var p = new BMap.Point(lng, lat);
    map.centerAndZoom(p, 15);
    var marker = new BMap.Marker(p); // ������
    // marker.addEventListener('click', function (e) { map.removeOverlay(e.target); })

    $.each(markerList, function (i, item) {
        map.removeOverlay(item);
    })

    map.addOverlay(marker);
    markerList.push(marker);
    reloadMap();
}


//�Զ���������˵�
var autoCompleteRequest;
function initialAutoComplete(vid, valueField, textField) {
    $('#' + vid).combobox({
        valueField: valueField, //'MDN', //TPrice
        textField: textField, //'MDN',
        //ע���¼�
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
            var thisKey = encodeURIComponent($('#' + vid).combobox('getText')); //������
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
                        alert("���ݼ��ش���");
                    }
                }
            });
        }
    });
};

//ȫ��
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

//�򿪻��߹رնԱ����
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

//ȷ���Ա�
function confirmDiff() {

    var beginDate = getSelectDate($('#beginDate').datetimebox('getValue'));
    var endDate = getSelectDate($('#endDate').datetimebox('getValue'));

    $('#lblBeginDate').text(getHistoryDate(beginDate, 'beginDate'));
    $('#lblEndDate').text(getHistoryDate(endDate, 'endDate'));

    if ($('#beginMode').val() == $('#endMode').val()) {
        if (endDate <= beginDate) { alert("����ʱ�������ڿ�ʼʱ��"); return; }
    }

    $('#diff-select-control').hide();
    $('#diff-control').show();

    initColorBar();
    reloadMap();
}