﻿app.directive("datetimepicker", function () {
    return {
        restrict: 'ECMA',
        template: '<div class="input-group date form_date col-md-5" data-date="" data-date-format="yyyy-mm-dd"\
                                 data-link-format="yyyy-mm-dd">\
                                <input class="form-control" size="16" type="text" ng-model="ngModel" readonly>\
                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>\
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>\
                            </div>',
        scope: {
            ngModel: '='
        }
    };
});