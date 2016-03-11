angular.module('parametersMap', ['myApp.region', 'myApp.parameters'])
    .factory('parametersMapService', function (baiduMapService, networkElementService, geometryService) {
        return {
            showElementsInOneTown: function(city, district, town, showENodebInfo, showCellInfo) {
                networkElementService.queryENodebsInOneTown(city, district, town).then(function (eNodebs) {
                    geometryService.transformToBaidu(eNodebs[0].longtitute, eNodebs[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - eNodebs[0].longtitute;
                        var yOffset = coors.y - eNodebs[0].lattitute;
                        baiduMapService.setCellFocus(coors.x, coors.y, 16);
                        for (var i = 0; i < eNodebs.length; i++) {
                            eNodebs[i].longtitute += xOffset;
                            eNodebs[i].lattitute += yOffset;
                            var marker = baiduMapService.generateIconMarker(eNodebs[i].longtitute, eNodebs[i].lattitute,
                                "/Content/Images/Hotmap/site_or.png");
                            baiduMapService.addOneMarkerToScope(marker, showENodebInfo, eNodebs[i]);
                            networkElementService.queryCellInfosInOneENodeb(eNodebs[i].eNodebId).then(function(cells) {
                                for (var j = 0; j < cells.length; j++) {
                                    cells[j].longtitute += xOffset;
                                    cells[j].lattitute += yOffset;
                                    var cellSector = baiduMapService.generateSector(cells[j]);
                                    baiduMapService.addOneSectorToScope(cellSector, showCellInfo, cells[j]);
                                }
                            });
                        }
                    });
                });
            }
        }
    });