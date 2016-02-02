
var getDistance = function(p1Lat, p1Lng, p2Lat, p2Lng) {
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

var getLonLat = function(centre, x, y) {
    var lat = centre.lat + y / getDistance(centre.lat, centre.lng, centre.lat + 1, centre.lng);
    var lng = centre.lng + x / getDistance(centre.lat, centre.lng, centre.lat, centre.lng + 1);
    return new BMap.Point(lng, lat);
};

var getPosition = function(centre, r, angle) {
    var x = r * Math.cos(angle * Math.PI / 180);
    var y = r * Math.sin(angle * Math.PI / 180);
    return getLonLat(centre, x, y);
};

///
// 计算根据中心点偏移一定半径和角度的点，用于画扇区
// centre: 中心点
// irotation: 方位角
// iangle: 水平波瓣宽度
// zoom: 当前百度地图缩放级别
var generateSectorPolygonPoints = function(centre, irotation, iangle, zoom) {
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
};

var getRadius = function(zoom) {
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

var getDtPointRadius = function (zoom) {
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
