app
    .controller('FormDateController', function() {

        $('.form_date').datetimepicker({
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });

    })
    .directive("datetimepicker", function() {
        return {
            restrict: 'ECMA',
            controller: 'FormDateController',
            template: '<div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd"\
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