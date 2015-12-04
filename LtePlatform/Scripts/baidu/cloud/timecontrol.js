var TimeControl = {
    playing: false, currentTimes: 0, times: 8
       , progress: 0, timeoutID: 0, interval: 70, pace: 0
       , $progress: $("html"), $el: $("html"), $btn: $("html")
       , mode: "" //模式：day、weeks、history
       , maxTime: 0 //最大刻度
       , maxDay: null //最大日期
       , currentDay: null //当前刻度对应日期
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


function createDateBox(date) {
    var ctr = $('<input id="dateBox" type="text" style="width:100px"/>');
    $.parser.parse(ctr);
    $(ctr).datebox({
        required: true,
        formatter: function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            var h = date.getHours();
            return m + '/' + d + '/' + y + " " + h + ":00";
        }
    });
    $(ctr).datebox('setValue', date);
    $(ctr).show();
    return ctr;
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
        }else{
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
    //设置当前日期为刻度开始日期

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

            //当前日期累加1个刻度的时间
            if (this.mode == "weeks") {
                this.currentDay = new Date(this.currentDay.setDate(this.currentDay.getDate() + 1));//加1天
            } else {
                this.currentDay = new Date(this.currentDay.getTime() + 60 * 60 * 1000);//加1小时
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
    TimeControl.progress = ((time + (time < 5 ? 0.4 : 0.25)) / TimeControl.times) * 1000;//计算进度条大概位置


    TimeControl.intTime(time);
    TimeControl.updateLayer(TimeControl.timeList[time]);
}
