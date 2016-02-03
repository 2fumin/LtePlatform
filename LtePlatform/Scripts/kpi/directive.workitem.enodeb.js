app.directive('workitemEnodeb', function() {
    return {
        restrict: 'EA',
        replace: true,
        template: '<table class="table table-hover">\
                    <tbody>\
                        <tr>\
                            <td>基站名称</td>\
                            <td>{{eNodebDetails.name}}</td>\
                            <td>厂家</td>\
                            <td>{{eNodebDetails.factory}}</td>\
                            <td>规划编码</td>\
                            <td>{{eNodebDetails.planNum}}</td>\
                        </tr>\
                        <tr>\
                            <td>经度</td>\
                            <td>{{eNodebDetails.longtitute}}</td>\
                            <td>纬度</td>\
                            <td>{{eNodebDetails.lattitute}}</td>\
                            <td>开通日期</td>\
                            <td>{{eNodebDetails.openDate}}</td>\
                        </tr>\
                        <tr>\
                            <td>基站地址</td>\
                            <td colspan="5">{{eNodebDetails.address}}</td>\
                        </tr>\
                    </tbody>\
        </table>'
    };
});