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

var mapLonLatEdits = function (sourceList, destList) {
    for (var i = 0; i < destList.length; i++) {
        if (isLongtituteValid(destList[i])) {
            mapLonLat(sourceList[destList[i].index], destList[i]);
        }
    }
};
