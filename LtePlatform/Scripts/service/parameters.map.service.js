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
                    showCdmaElements(btss, showBtsInfo, showCellInfo);
                });
            },
            showENodebsElements: function(eNodebs, showENodebInfo, showCellInfo) {
                return showENodebsElements(eNodebs, showENodebInfo, showCellInfo);
            }
        }
    })
    .factory('parametersDialogService', function ($uibModal, $log) {
        return {
            showENodebInfo: function(eNodeb) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/Parameters/Map/ENodebMapInfoBox.html',
                    controller: 'map.eNodeb.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function() {
                            return eNodeb.name + "-" + "基站基本信息";
                        },
                        eNodeb: function() {
                            return eNodeb;
                        }
                    }
                });
                modalInstance.result.then(function(info) {
                    console.log(info);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showBtsInfo: function (bts) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/Parameters/Map/BtsMapInfoBox.html',
                    controller: 'map.bts.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function () {
                            return bts.name + "-" + "基站基本信息";
                        },
                        bts: function () {
                            return bts;
                        }
                    }
                });
                modalInstance.result.then(function (info) {
                    console.log(info);
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showCellInfo: function(cell) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/Rutrace/Map/NeighborMapInfoBox.html',
                    controller: 'map.neighbor.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function() {
                            return cell.cellName + "小区信息";
                        },
                        neighbor: function() {
                            return cell;
                        }
                    }
                });
                modalInstance.result.then(function(nei) {
                    console.log(nei);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            }
        };
    });