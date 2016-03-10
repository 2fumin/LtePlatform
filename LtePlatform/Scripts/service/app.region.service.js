angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function ($q, $http, appUrlService) {
        return {
            initializeCities: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList')
                }).success(function (result) {
                    deferred.resolve({
                        options: result,
                        selected: result[0]
                    });
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryDistricts: function (cityName) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    params: {
                        city: cityName
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            queryTowns: function (cityName, districtName) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    params: {
                        city: cityName,
                        district: districtName
                    }
                }).success(function (result) {
                    deferred.resolve(result);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            }
        };
    })
    .factory('appFormatService', function() {
        return {
            getDate: function (strDate) {
                var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/,
                    function (a) { return parseInt(a, 10) - 1; }).match(/\d+/g) + ')');
                return date;
            },
            getDateString: function(dateTime, fmt) {
                var o = {
                    "M+": dateTime.getMonth() + 1, //月份 
                    "d+": dateTime.getDate(), //日 
                    "h+": dateTime.getHours(), //小时 
                    "m+": dateTime.getMinutes(), //分 
                    "s+": dateTime.getSeconds(), //秒 
                    "q+": Math.floor((dateTime.getMonth() + 3) / 3), //季度 
                    "S": dateTime.getMilliseconds() //毫秒 
                };
                if (/(y+)/.test(fmt))
                    fmt = fmt.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
                for (var k in o)
                    if (o.hasOwnProperty(k))
                        if (new RegExp("(" + k + ")").test(fmt))
                            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                return fmt;
            },
            lowerFirstLetter: function(str) {
                return str.substring(0, 1).toLowerCase() +
                    str.substring(1);
            }
        }
    })
    .factory('geometryService', function ($http, $q) {
        var getDistanceFunc = function(p1Lat, p1Lng, p2Lat, p2Lng) {
            var earthRadiusKm = 6378.137;
            var dLat1InRad = p1Lat * (Math.PI / 180);
            var dLong1InRad = p1Lng * (Math.PI / 180);
            var dLat2InRad = p2Lat * (Math.PI / 180);
            var dLong2InRad = p2Lng * (Math.PI / 180);
            var dLongitude = dLong2InRad - dLong1InRad;
            var dLatitude = dLat2InRad - dLat1InRad;
            var a = Math.pow(Math.sin(dLatitude / 2), 2) + Math.cos(dLat1InRad) * Math.cos(dLat2InRad) * Math.pow(Math.sin(dLongitude / 2), 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var dDistance = earthRadiusKm * c;
            return dDistance;
        };
        var getLonLatFunc = function (centre, x, y) {
            var lat = centre.lat + y / getDistanceFunc(centre.lat, centre.lng, centre.lat + 1, centre.lng);
            var lng = centre.lng + x / getDistanceFunc(centre.lat, centre.lng, centre.lat, centre.lng + 1);
            return new BMap.Point(lng, lat);
        };
        var getPositionFunc = function(centre, r, angle) {
            var x = r * Math.cos(angle * Math.PI / 180);
            var y = r * Math.sin(angle * Math.PI / 180);
            return getLonLatFunc(centre, x, y);
        };
        var getRadiusFunc = function(zoom) {
            var rSation = 70;
            var rSector = 0.2;
            switch (zoom) {
            case 15:
                rSector = rSector * 0.75;
                rSation = rSation * 0.75;
                break;
            case 16:
                rSector = rSector / 2.5;
                rSation = rSation / 2.5;
                break;
            case 17:
                rSector = rSector / 5;
                rSation = rSation / 5;
                break;
            default:
                break;
            }

            return { rSector: rSector, rStation: rSation };
        };
        var myKey = 'LlMnTd7NcCWI1ibhDAdKeVlG';
        var baiduApiUrl = '//api.map.baidu.com/geoconv/v1/?callback=JSON_CALLBACK';
        return {
            getDistance: function(p1Lat, p1Lng, p2Lat, p2Lng) {
                return getDistanceFunc(p1Lat, p1Lng, p2Lat, p2Lng);
            },
            getLonLat: function(centre, x, y) {
                return getLonLatFunc(centre, x, y);
            },
            getPosition: function(centre, r, angle) {
                return getPositionFunc(centre, r, angle);
            },
            generateSectorPolygonPoints: function(centre, irotation, iangle, zoom) {
                var assemble = [];
                var dot;
                var i;
                var r = getRadiusFunc(zoom).rSector;

                for (i = 0; i <= r; i += r / 2) {
                    dot = getPositionFunc(centre, i, irotation);
                    assemble.push(dot);
                }

                for (i = 0; i <= iangle; i += iangle / 5) {
                    dot = getPositionFunc(centre, r, i + irotation);
                    assemble.push(dot);
                }

                return assemble;
            },
            getRadius: function(zoom) {
                return getRadiusFunc(zoom);
            },
            getDtPointRadius: function (zoom) {
                var radius = 17;
                switch (zoom) {
                    case 15:
                        radius *= 0.9;
                        break;
                    case 16:
                        radius *= 0.8;
                        break;
                    case 17:
                        radius *= 0.75;
                        break;
                    default:
                        break;
                }
                return radius;
            },
            transformToBaidu: function (longtitute, lattitute) {
                var deferred = $q.defer();
                $http.jsonp(baiduApiUrl + '&coords='+ longtitute + ',' + lattitute
                        + '&from=1&to=5&ak=' + myKey).success(function (result) {
                    deferred.resolve(result.result[0]);
                })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            transformBaiduCoors: function (coors) {
                var deferred = $q.defer();
                $http.jsonp(baiduApiUrl + '&coords=' + coors.longtitute + ',' + coors.lattitute
                    + '&from=1&to=5&ak=' + myKey).success(function(result) {
                    coors.longtitute = result.result[0].x;
                    coors.lattitute = result.result[0].y;
                    deferred.resolve(coors);
                    })
                .error(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;;
            }
        };
    })
    .factory('baiduMapService', function (geometryService) {
        var map = {};
        return {
            initializeMap: function(tag, zoomLevel) {
                map = new BMap.Map(tag);
                map.centerAndZoom(new BMap.Point(113.01, 23.02), zoomLevel);
                map.setMinZoom(8); //设置地图最小级别
                map.setMaxZoom(17); //设置地图最大级别

                map.enableScrollWheelZoom(); //启用滚轮放大缩小
                map.enableDragging();
                map.disableDoubleClickZoom();

                var topLeftControl = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }); // 左上角，添加比例尺
                var topLeftNavigation = new BMap.NavigationControl(); //左上角，添加默认缩放平移控件

                map.addControl(topLeftControl);
                map.addControl(topLeftNavigation);
            },
            addCityBoundary: function(city) {
                var bdary = new BMap.Boundary();
                bdary.get(city, function(rs) { //获取行政区域
                    var count = rs.boundaries.length; //行政区域的点有多少个
                    if (count === 0) {
                        return;
                    }
                    for (var i = 0; i < count; i++) {
                        var ply = new BMap.Polygon(rs.boundaries[i], {
                            strokeWeight: 2,
                            strokeColor: "#ff0000",
                            fillOpacity: 0.1
                        }); //建立多边形覆盖物
                        map.addOverlay(ply); //添加覆盖物
                    }
                });
            },
            removeOverlay: function(overlay) {
                map.removeOverlay(overlay);
            },
            removeOverlays: function(overlays) {
                for (var i = 0; i < overlays.length; i++) {
                    map.removeOverlay(overlays[i]);
                }
            },
            clearOverlays: function() {
                map.clearOverlays();
            },
            addOneMarker: function(marker, html) {
                map.addOverlay(marker);
                var infoBox = new BMapLib.InfoBox(map, html, {
                    boxStyle: {
                        background: "url('/Content/themes/baidu/tipbox.jpg') no-repeat center top",
                        width: "270px",
                        height: "200px"
                    },
                    closeIconUrl: "/Content/themes/baidu/close.png",
                    closeIconMargin: "1px 1px 0 0",
                    enableAutoPan: true,
                    align: INFOBOX_AT_TOP
                });
                marker.addEventListener("click", function () {

                    infoBox.open(this);
                });
            },
            addOneMarkerToScope: function (marker, callback, data) {
                map.addOverlay(marker);
                marker.addEventListener("click", function () {
                    callback(data);
                });
            },
            addOneSector: function(sector, html, boxHeight) {
                boxHeight = boxHeight || "300px";
                map.addOverlay(sector);
                var infoBox = new BMapLib.InfoBox(map, html, {
                    boxStyle: {
                        background: "url('/Content/themes/baidu/tipbox.jpg') no-repeat center top",
                        width: "270px",
                        height: boxHeight
                    },
                    closeIconUrl: "/Content/themes/baidu/close.png",
                    closeIconMargin: "1px 1px 0 0",
                    enableAutoPan: true,
                    align: INFOBOX_AT_TOP
                });
                sector.addEventListener("click", function () {
                    infoBox.open(this.getPath()[2]);
                });
            },
            addOneSectorToScope: function(sector, callBack, neighbor) {
                map.addOverlay(sector);
                sector.addEventListener("click", function () {
                    callBack(neighbor);
                });
            },
            setCellFocus: function (longtitute, lattitute, zoomLevel) {
                zoomLevel = zoomLevel || 15;
                map.centerAndZoom(new BMap.Point(longtitute, lattitute), zoomLevel);
            },
            generateSector: function (data, sectorColor) {                
                var center = { lng: data.longtitute, lat: data.lattitute };
                var iangle = 65;
                var irotation = data.azimuth - iangle / 2;
                var zoom = map.getZoom();
                var points = geometryService.generateSectorPolygonPoints(center, irotation, iangle, zoom);
                sectorColor = sectorColor || "blue";
                var sector = new BMap.Polygon(points, {
                    strokeWeight: 2,
                    strokeColor: sectorColor,
                    fillColor: sectorColor,
                    fillOpacity: 0.5
                });
                return sector;
            },
            getCurrentMapRange: function(xOffset, yOffset) {
                return {
                    west: map.getBounds().getSouthWest().lng + xOffset,
                    south: map.getBounds().getSouthWest().lat + yOffset,
                    east: map.getBounds().getNorthEast().lng + xOffset,
                    north: map.getBounds().getNorthEast().lat + yOffset
                };
            },
            generateIconMarker: function(longtitute, lattitute, iconUrl) {
                var icon = new BMap.Icon(iconUrl, new BMap.Size(20, 30));
                return new BMap.Marker(new BMap.Point(longtitute, lattitute), {
                    icon: icon
                });
            }
        };
    });