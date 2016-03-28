angular.module('college.map', ['myApp.region', 'college'])
    .factory('collegeMapService', function(baiduMapService, collegeService) {
        return {
            showCollegeInfos: function(showCollegeDialogs) {
                collegeService.queryStats().then(function(colleges) {
                    angular.forEach(colleges, function (college) {
                        var center;
                        collegeService.queryRegion(college.id).then(function(region) {
                            switch (region.regionType) {
                            case 2:
                                center = baiduMapService.drawPolygonAndGetCenter(region.info);
                                break;
                            case 1:
                                center = baiduMapService.drawRectangleAndGetCenter(region.info);
                                break;
                            default:
                                center = baiduMapService.drawCircleAndGetCenter(region.info);
                                break;
                            }
                            var marker = baiduMapService.generateMarker(center.X, center.Y);
                            baiduMapService.addOneMarkerToScope(marker, showCollegeDialogs, college);
                        });
                    });
                });
            }
        };
    });