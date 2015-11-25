function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";

    self.cityListUrl = "/api/CityList";
    self.kpiDataListUrl = "/api/KpiDataList";
    self.preciseStatUrl = "/api/PreciseStat/";
    self.preciseRegionUrl = "/api/PreciseRegion/";

    self.collegeQueryUrl = "/api/CollegeQuery";
    self.collegeRegionUrl = "/api/CollegeRegion";
    self.collegeStatUrl = "/api/CollegeStat";
    self.collegeENodebUrl = "/api/CollegeENodeb/";
    self.collegeBtssUrl = "/api/CollegeBtss/";
    self.college3GTestUrl = "/api/College3GTest/";
    self.college4GTestUrl = "/api/College4GTest/";
    self.collegeKpiUrl = "/api/CollegeKpi/";
    self.collegePreciseUrl = "/api/CollegePrecise/";
    self.collegeCellsUrl = "/api/CollegeCells/";
    self.collegeCdmaCellsUrl = "/api/CollegeCdmaCells/";

    self.cellUrl = "/api/Cell/";

    // Route operations

    // Other private operations

    // Operations
    self.loadAlarms = function (id) {
        sendRequest("/api/Alarms/", "GET", { eNodebId: id }, function (result) {
            model.alarms(result);
            $(".modal").modal("show");
        });
    };

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
