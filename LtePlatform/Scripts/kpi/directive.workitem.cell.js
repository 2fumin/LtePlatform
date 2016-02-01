﻿app.directive('workitemCell', function () {
    return {
        restrict: 'EA',
        replace: true,
        template: '<table class="table table-hover">\
                    <tbody>\
                        <tr>\
                            <td>频点</td>\
                            <td>{{lteCellDetails.frequency}}</td>\
                            <td>经度</td>\
                            <td>{{lteCellDetails.longtitute}}</td>\
                            <td>纬度</td>\
                            <td>{{lteCellDetails.lattitute}}</td>\
                        </tr>\
                        <tr>\
                            <td>挂高</td>\
                            <td>{{lteCellDetails.height}}</td>\
                            <td>方位角</td>\
                            <td>{{lteCellDetails.azimuth}}</td>\
                            <td>下倾角</td>\
                            <td>{{lteCellDetails.downTilt}}</td>\
                        </tr>\
                        <tr>\
                            <td>室内外</td>\
                            <td>{{lteCellDetails.indoor}}</td>\
                            <td>天线增益</td>\
                            <td>{{lteCellDetails.antennaGain}}</td>\
                            <td>RS功率</td>\
                            <td>{{lteCellDetails.rsPower}}</td>\
                        </tr>\
                        <tr>\
                            <td>PCI</td>\
                            <td>{{lteCellDetails.pci}}</td>\
                            <td>PRACH</td>\
                            <td>{{lteCellDetails.prach}}</td>\
                            <td>TAC</td>\
                            <td>{{lteCellDetails.tac}}</td>\
                        </tr>\
                    </tbody>\
        </table>'
    };
});