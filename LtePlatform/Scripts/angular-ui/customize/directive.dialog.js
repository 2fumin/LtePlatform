app.directive('dialog', function() {
    return {
        restrict: 'EA',
        template: '<div class="modal fade">\
            <div class="modal-dialog">\
                <div class="modal-content">\
                    <div class="modal-header">\
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">\
                            &times;\
                        </button>\
                        <h4 class="modal-title text-primary">{{dialogTitle}}</h4>\
                    </div>\
                    <div class="modal-body">\
                        <span ng-transclude></span>\
                    </div>\
                    <div class="modal-footer">\
                        <button class="btn btn-default" data-dismiss="modal">关闭</button>\
                    </div>\
                </div>\
            </div>\
        </div>',
        transclude: true
    };
});