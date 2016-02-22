angular.module('myApp.region', ['myApp.url'])
    .factory('appRegionService', function ($q, $http, appUrlService) {
        return {
            initializeCities: function () {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl('CityList'),
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
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
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    },
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
            }
        }
    })
    .factory('geometryService', function() {
        var getLonLatFunc = function (centre, x, y) {
            var lat = centre.lat + y / getDistance(centre.lat, centre.lng, centre.lat + 1, centre.lng);
            var lng = centre.lng + x / getDistance(centre.lat, centre.lng, centre.lat, centre.lng + 1);
            return new BMap.Point(lng, lat);
        };
        return {
            getDistance: function(p1Lat, p1Lng, p2Lat, p2Lng) {
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
            },
            getLonLat: function(centre, x, y) {
                return getLonLatFunc(centre, x, y);
            },
            getPosition: function(centre, r, angle) {
                var x = r * Math.cos(angle * Math.PI / 180);
                var y = r * Math.sin(angle * Math.PI / 180);
                return getLonLatFunc(centre, x, y);
            },
            generateSectorPolygonPoints: function(centre, irotation, iangle, zoom) {
                var assemble = [];
                var dot;
                var i;
                var r = getRadius(zoom).rSector;

                for (i = 0; i <= r; i += r / 2) {
                    dot = getPosition(centre, i, irotation);
                    assemble.push(dot);
                }

                for (i = 0; i <= iangle; i += iangle / 5) {
                    dot = getPosition(centre, r, i + irotation);
                    assemble.push(dot);
                }

                return assemble;
            },
            getRadius: function(zoom) {
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
            }
        };
    })
    .factory('baiduMapService', function() {
        var map = {};
        return {
            initializeMap: function(tag, zoomLevel) {
                map = new BMap.Map(tag);
                map.centerAndZoom("佛山", zoomLevel);
                map.setMinZoom(8); //设置地图最小级别
                map.setMaxZoom(17); //设置地图最大级别

                map.enableScrollWheelZoom(); //启用滚轮放大缩小
                map.enableDragging();
                map.disableDoubleClickZoom();

                var bdary = new BMap.Boundary();
                bdary.get("佛山市", function (rs) { //获取行政区域
                    var count = rs.boundaries.length; //行政区域的点有多少个
                    if (count === 0) {
                        alert('未能获取当前输入行政区域');
                        return;
                    }
                    var pointArray = [];
                    for (var i = 0; i < count; i++) {
                        var ply = new BMap.Polygon(rs.boundaries[i], {
                            strokeWeight: 2,
                            strokeColor: "#ff0000",
                            fillOpacity: 0.1
                        }); //建立多边形覆盖物
                        map.addOverlay(ply); //添加覆盖物
                        pointArray = pointArray.concat(ply.getPath());
                    }
                    map.setViewport(pointArray); //调整视野                 
                });

                var topLeftControl = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }); // 左上角，添加比例尺
                var topLeftNavigation = new BMap.NavigationControl(); //左上角，添加默认缩放平移控件

                map.addControl(topLeftControl);
                map.addControl(topLeftNavigation);
            }
        };
    });