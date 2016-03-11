angular.module('parametersMap', ['myApp.region', 'myApp.parameters'])
    .factory('parametersMapService', function (baiduMapService, networkElementService, geometryService) {
        var showSectors = function(queryFunc, eNodebId, xOffset, yOffset, showCellInfo) {
            queryFunc(eNodebId).then(function(cells) {
                for (var j = 0; j < cells.length; j++) {
                    cells[j].longtitute += xOffset;
                    cells[j].lattitute += yOffset;
                    var cellSector = baiduMapService.generateSector(cells[j]);
                    baiduMapService.addOneSectorToScope(cellSector, showCellInfo, cells[j]);
                }
            });
        };
        var showENodebsElements = function(eNodebs, showENodebInfo, showCellInfo) {
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
                    showSectors(networkElementService.queryCellInfosInOneENodeb, eNodebs[i].eNodebId, xOffset, yOffset, showCellInfo);
                }
            });
        };
        var showCdmaElements = function (btss, showBtsInfo, showCellInfo) {
            geometryService.transformToBaidu(btss[0].longtitute, btss[0].lattitute).then(function (coors) {
                var xOffset = coors.x - btss[0].longtitute;
                var yOffset = coors.y - btss[0].lattitute;
                baiduMapService.setCellFocus(coors.x, coors.y, 16);
                for (var i = 0; i < btss.length; i++) {
                    btss[i].longtitute += xOffset;
                    btss[i].lattitute += yOffset;
                    var marker = baiduMapService.generateIconMarker(btss[i].longtitute, btss[i].lattitute,
                        "/Content/Images/Hotmap/site_bl.png");
                    baiduMapService.addOneMarkerToScope(marker, showBtsInfo, btss[i]);
                    showSectors(networkElementService.queryCdmaCellInfosInOneBts, btss[i].btsId, xOffset, yOffset, showCellInfo);
                }
            });
        };
        return {
            showElementsInOneTown: function(city, district, town, showENodebInfo, showCellInfo) {
                networkElementService.queryENodebsInOneTown(city, district, town).then(function (eNodebs) {
                    showENodebsElements(eNodebs, showENodebInfo, showCellInfo);
                });
            },
            showElementsWithGeneralName: function(name, showENodebInfo, showCellInfo) {
                networkElementService.queryENodebsByGeneralName(name).then(function(eNodebs) {
                    if (eNodebs.length === 0) return;
                    showENodebsElements(eNodebs, showENodebInfo, showCellInfo);
                });
            },
            showCdmaInOneTown: function (city, district, town, showBtsInfo, showCellInfo) {
                networkElementService.queryBtssInOneTown(city, district, town).then(function (btss) {
                    showCdmaElements(btss, showBtsInfo, showCellInfo);
                });
            },
            showCdmaWithGeneralName: function (name, showBtsInfo, showCellInfo) {
                networkElementService.queryBtssByGeneralName(name).then(function (btss) {
                    if (btss.length === 0) return;
                    showCdmaElements(eNodebs, showBtsInfo, showCellInfo);
                });
            }
        }
    });