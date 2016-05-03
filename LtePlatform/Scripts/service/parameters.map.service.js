angular.module('parametersMap', ['myApp.region', 'myApp.parameters'])
    .factory('parametersMapService', function (baiduMapService, networkElementService, geometryService) {
        var showCellSectors = function(cells, showCellInfo, xOffset, yOffset) {
            angular.forEach(cells, function(cell) {
                cell.longtitute += xOffset;
                cell.lattitute += yOffset;
                var cellSector = baiduMapService.generateSector(cell);
                baiduMapService.addOneSectorToScope(cellSector, showCellInfo, cell);
            });
        };
        var showSectors = function(queryFunc, eNodebId, xOffset, yOffset, showCellInfo) {
            queryFunc(eNodebId).then(function(cells) {
                showCellSectors(cells, showCellInfo, xOffset, yOffset);
            });
        };
        var showENodebsElements = function(eNodebs, showENodebInfo, showCellInfo) {
            geometryService.transformToBaidu(eNodebs[0].longtitute, eNodebs[0].lattitute).then(function(coors) {
                var xOffset = coors.x - eNodebs[0].longtitute;
                var yOffset = coors.y - eNodebs[0].lattitute;
                baiduMapService.setCellFocus(coors.x, coors.y, 16);
                angular.forEach(eNodebs, function(eNodeb) {
                    eNodeb.longtitute += xOffset;
                    eNodeb.lattitute += yOffset;
                    var marker = baiduMapService.generateIconMarker(eNodeb.longtitute, eNodeb.lattitute,
                        "/Content/Images/Hotmap/site_or.png");
                    baiduMapService.addOneMarkerToScope(marker, showENodebInfo, eNodeb);
                    if (showCellInfo !== undefined)
                        showSectors(networkElementService.queryCellInfosInOneENodeb, eNodeb.eNodebId, xOffset, yOffset, showCellInfo);
                });
            });
        };
        var showPhpElements = function (elements, showElementInfo) {
            geometryService.transformToBaidu(elements[0].longtitute, elements[0].lattitute).then(function (coors) {
                var xOffset = coors.x - parseFloat(elements[0].longtitute);
                var yOffset = coors.y - parseFloat(elements[0].lattitute);
                baiduMapService.setCellFocus(coors.x, coors.y, 16);
                angular.forEach(elements, function (element) {
                    element.longtitute = xOffset + parseFloat(element.longtitute);
                    element.lattitute = yOffset + parseFloat(element.lattitute);
                    var marker = baiduMapService.generateIconMarker(element.longtitute, element.lattitute,
                        "/Content/Images/Hotmap/site_or.png");
                    baiduMapService.addOneMarkerToScope(marker, showElementInfo, element);
                });
            });
        };
        var showCdmaElements = function (btss, showBtsInfo, showCellInfo) {
            geometryService.transformToBaidu(btss[0].longtitute, btss[0].lattitute).then(function (coors) {
                var xOffset = coors.x - btss[0].longtitute;
                var yOffset = coors.y - btss[0].lattitute;
                baiduMapService.setCellFocus(coors.x, coors.y, 16);
                angular.forEach(btss, function(bts) {
                    bts.longtitute += xOffset;
                    bts.lattitute += yOffset;
                    var marker = baiduMapService.generateIconMarker(bts.longtitute, bts.lattitute,
                        "/Content/Images/Hotmap/site_bl.png");
                    baiduMapService.addOneMarkerToScope(marker, showBtsInfo, bts);
                    if (showCellInfo !== undefined)
                        showSectors(networkElementService.queryCdmaCellInfosInOneBts, bts.btsId, xOffset, yOffset, showCellInfo);
                });
            });
        };
        return {
            showElementsInOneTown: function(city, district, town, showENodebInfo, showCellInfo) {
                networkElementService.queryENodebsInOneTown(city, district, town).then(function(eNodebs) {
                    showENodebsElements(eNodebs, showENodebInfo, showCellInfo);
                });
            },
            showElementsWithGeneralName: function(name, showENodebInfo, showCellInfo) {
                networkElementService.queryENodebsByGeneralName(name).then(function(eNodebs) {
                    if (eNodebs.length === 0) return;
                    showENodebsElements(eNodebs, showENodebInfo, showCellInfo);
                });
            },
            showCdmaInOneTown: function(city, district, town, showBtsInfo, showCellInfo) {
                networkElementService.queryBtssInOneTown(city, district, town).then(function(btss) {
                    showCdmaElements(btss, showBtsInfo, showCellInfo);
                });
            },
            showCdmaWithGeneralName: function(name, showBtsInfo, showCellInfo) {
                networkElementService.queryBtssByGeneralName(name).then(function(btss) {
                    if (btss.length === 0) return;
                    showCdmaElements(btss, showBtsInfo, showCellInfo);
                });
            },
            showENodebsElements: function(eNodebs, showENodebInfo) {
                return showENodebsElements(eNodebs, showENodebInfo);
            },
            showBtssElements: function(btss, showBtsInfo) {
                return showCdmaElements(btss, showBtsInfo);
            },
            showCellSectors: function(cells, showCellInfo) {
                geometryService.transformToBaidu(cells[0].longtitute, cells[0].lattitute).then(function(coors) {
                    var xOffset = coors.x - cells[0].longtitute;
                    var yOffset = coors.y - cells[0].lattitute;
                    baiduMapService.setCellFocus(coors.x, coors.y, 16);
                    showCellSectors(cells, showCellInfo, xOffset, yOffset);
                });
            },
            showPhpElements: function(elements, showElementInfo) {
                return showPhpElements(elements, showElementInfo);
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
            showDistributionInfo: function (distribution) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/Parameters/Map/DistributionMapInfoBox.html',
                    controller: 'map.distribution.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function () {
                            return distribution.name + "-" + "室内分布基本信息";
                        },
                        distribution: function () {
                            return distribution;
                        }
                    }
                });
                modalInstance.result.then(function (info) {
                    console.log(info);
                }, function () {
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
            },
            showCollegeCellInfo: function (cell) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/College/Table/CollegeCellInfoBox.html',
                    controller: 'college.cell.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function () {
                            return cell.eNodebName + "-" + cell.sectorId + "小区信息";
                        },
                        cell: function () {
                            return cell;
                        }
                    }
                });
                modalInstance.result.then(function (nei) {
                    console.log(nei);
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showCdmaCellInfo: function (cell) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/Parameters/Map/CdmaCellInfoBox.html',
                    controller: 'map.cdma.cell.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function () {
                            return cell.cellName + "小区信息";
                        },
                        neighbor: function () {
                            return cell;
                        }
                    }
                });
                modalInstance.result.then(function (nei) {
                    console.log(nei);
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showCollegeCdmaCellInfo: function (cell) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/appViews/College/Table/CdmaCellInfoBox.html',
                    controller: 'map.cdma.cell.dialog',
                    size: 'sm',
                    resolve: {
                        dialogTitle: function () {
                            return cell.btsName + "-" + cell.sectorId + "小区信息";
                        },
                        neighbor: function () {
                            return cell;
                        }
                    }
                });
                modalInstance.result.then(function (nei) {
                    console.log(nei);
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            }
        };
    });