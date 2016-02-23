angular.module('ne.geometry', [])
    .factory('neGeometryService', function () {
        var isLongtituteValid = function (longtitute) {
            return (!isNaN(longtitute)) && longtitute > 112 && longtitute < 114;
        };

        var isLattituteValid = function (lattitute) {
            return (!isNaN(lattitute)) && lattitute > 22 && lattitute < 24;
        };

        var isLonLatValid = function (item) {
            return isLongtituteValid(item.longtitute) && isLattituteValid(item.lattitute);
        };

        var mapLonLat = function (source, destination) {
            source.longtitute = destination.longtitute;
            source.lattitute = destination.lattitute;
        };

        return {
            queryENodebLonLatEdits: function (eNodebs) {
                var result = [];
                for (var index = 0; index < eNodebs.length; index++) {
                    if (!isLonLatValid(eNodebs[index])) {
                        result.push({
                            index: index,
                            eNodebId: eNodebs[index].eNodebId,
                            name: eNodebs[index].name,
                            district: eNodebs[index].districtName,
                            town: eNodebs[index].townName,
                            longtitute: eNodebs[index].longtitute,
                            lattitute: eNodebs[index].lattitute
                        });
                    }
                }
                return result;
            },
            queryCellLonLatEdits: function(cells) {
                var result = [];
                for (var index = 0; index < cells.length; index++) {
                    if (!isLonLatValid(cells[index])) {
                        result.push({
                            index: index,
                            eNodebId: cells[index].eNodebId,
                            sectorId: cells[index].sectorId,
                            frequency: cells[index].frequency,
                            isIndoor: cells[index].isIndoor,
                            longtitute: cells[index].longtitute,
                            lattitute: cells[index].lattitute
                        });
                    }
                }
                return result;
            },
            mapLonLatEdits: function (sourceFunc, destList) {
                var sourceList = sourceFunc();
                for (var i = 0; i < destList.length; i++) {
                    destList[i].longtitute = parseFloat(destList[i].longtitute);
                    destList[i].lattitute = parseFloat(destList[i].lattitute);
                    if (isLonLatValid(destList[i])) {
                        console.log(destList[i]);
                        mapLonLat(sourceList[destList[i].index], destList[i]);
                    }
                }
                sourceFunc(sourceList);
            }
        };
    });