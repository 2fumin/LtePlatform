function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";

    self.cityListUrl = "/api/CityList";
    self.kpiDataListUrl = "/api/KpiDataList";

    self.collegeQueryUrl = "/api/CollegeQuery";
    self.collegeRegionUrl = "/api/CollegeRegion";
    self.collegeStatUrl = "/api/CollegeStat";
    self.collegeENodebUrl = "/api/CollegeENodeb/";
    self.college3GTestUrl = "/api/College3GTest/";
    self.college4GTestUrl = "/api/College4GTest/";
    self.collegeKpiUrl = "/api/CollegeKpi/";

    self.cellUrl = "/api/Cell/";

    // Route operations

    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;

    // Data access operations
    self.setAccessToken = function (accessToken) {
        sessionStorage.setItem("accessToken", accessToken);
    };

    self.getAccessToken = function () {
        return sessionStorage.getItem("accessToken");
    };
}
