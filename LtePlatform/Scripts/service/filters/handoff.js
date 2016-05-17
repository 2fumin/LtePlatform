angular.module('handoff.filters', [])
    .filter("timeToTrigger", function() {
        var durations = [
            0, 40, 64, 80, 100, 128, 160, 256, 320, 480, 512, 640, 1024, 1280, 2560, 5120
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 16 ? durations[input] : input;
        }
    })
    .filter("reportInterval", function() {
        var durations = [
            '120ms', '240ms', '480ms', '640ms', '1024ms', '2048ms', '5120ms', '10240ms', '1min', '6min', '12min', '30min', '60min'
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 13 ? durations[input] : 'illegal';
        }
    })
    .filter("reportAmount", function() {
        var amounts = [
            "1", "2", "4", "8", "16", "32", "64", "无限次"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 8 ? amounts[input] : input;
        }
    })
    .filter("huaweiEvent", function() {
        var amounts = [
            "A3", "A4", "A5"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 3 ? amounts[input] : input;
        }
    })
    .filter("zteIntraRatEvent", function() {
        var amounts = [
            "A1", "A2", "A3", "A4", "A5", "A6"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 6 ? amounts[input] : input;
        }
    })
        .filter("halfDb", function() {
        return function(input) {
            return angular.isNumber(input) ? input / 2 : input;
        };
    })
    .filter("triggerQuantity", function(){
        var types=["RSRP", "RSRQ"];
        return function(input){
            return input===0||input===1?types[input]:input;
        };
    })
    .filter("reportQuantity", function(){
        var types=["与触发量相同", "全部发送"];
        return function(input){
            return input===0||input===1?types[input]:input;
        };
    });
