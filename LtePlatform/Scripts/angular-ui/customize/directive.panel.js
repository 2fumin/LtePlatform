app.directive('panelInfo', function() {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            title: '=panelTitle'
        },
        template: '<div class="panel panel-info">\
            <div class="panel-heading">\
                <h3 class="panel-title">\
                    {{title}}\
                </h3>\
            </div>\
            <div class="panel-body">\
                <span ng-transclude></span>\
            </div>\
        </div>',
        transclude: true
    };
});

app.directive('panelPrimary', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            title: '=panelTitle'
        },
        template: '<div class="panel panel-primary">\
            <div class="panel-heading">\
                <h3 class="panel-title">\
                    {{title}}\
                </h3>\
            </div>\
            <div class="panel-body">\
                <span ng-transclude></span>\
            </div>\
        </div>',
        transclude: true
    };
});

app.directive('panelWarning', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            title: '=panelTitle'
        },
        template: '<div class="panel panel-warning">\
            <div class="panel-heading">\
                <h3 class="panel-title">\
                    {{title}}\
                </h3>\
            </div>\
            <div class="panel-body">\
                <span ng-transclude></span>\
            </div>\
        </div>',
        transclude: true
    };
});