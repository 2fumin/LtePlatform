app.directive('workitemBts', function () {
    return {
        restrict: 'EA',
        replace: true,
        template: '<table class="table table-hover">\
                    <tbody>\
                        <tr>\
                            <td>基站名称</td>\
                            <td>{{btsDetails.name}}</td>\
                            <td>经度</td>\
                            <td>{{btsDetails.longtitute}}</td>\
                            <td>纬度</td>\
                            <td>{{btsDetails.lattitute}}</td>\
                        </tr>\
                        <tr>\
                            <td>基站地址</td>\
                            <td colspan="5">{{btsDetails.address}}</td>\
                        </tr>\
                    </tbody>\
        </table>'
    };
});