function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";

    self.cityListUrl = "/api/CityList";
    self.kpiDataListUrl = "/api/KpiDataList";
    self.preciseStatUrl = "/api/PreciseStat/";
    self.preciseRegionUrl = "/api/PreciseRegion/";
    self.alarmCountUrl = "/api/AlarmCount";
    self.dumpAlarmUrl = "/api/DumpAlarm";
    self.topDrop2GUrl = "/api/TopDrop2G/";
    self.preciseImportUrl = "/api/PreciseImport";
    self.townPreciseImportUrl = "/api/TownPreciseImport";

    self.collegeQueryUrl = "/api/CollegeQuery";
    self.collegeRegionUrl = "/api/CollegeRegion";
    self.collegeStatUrl = "/api/CollegeStat";
    self.collegeENodebUrl = "/api/CollegeENodeb/";
    self.collegeBtssUrl = "/api/CollegeBtss/";
    self.collegeLteDistributionsUrl = "/api/CollegeLteDistributions/";
    self.collegeCdmaDistributionsUrl = "/api/CollegeCdmaDistributions/";
    self.college3GTestUrl = "/api/College3GTest/";
    self.college4GTestUrl = "/api/College4GTest/";
    self.collegeKpiUrl = "/api/CollegeKpi/";
    self.collegePreciseUrl = "/api/CollegePrecise/";
    self.collegeCellsUrl = "/api/CollegeCells/";
    self.collegeCdmaCellsUrl = "/api/CollegeCdmaCells/";
    self.collegeAlarmUrl = "/api/CollegeAlarm";

    self.cellUrl = "/api/Cell/";
    self.cdmaCellUrl = "/api/CdmaCell";
    self.eNodebUrl = "/api/ENodeb";
    self.btsUrl = "/api/Bts";
    self.newENodebExcelsUrl = "/api/NewENodebExcels";
    self.newCellExcelsUrl = "/api/NewCellExcels";
    self.newBtsExcelsUrl = "/api/NewBtsExcels";
    self.newCdmaCellExcelsUrl = "/api/NewCdmaCellExcels";
    self.dumpENodebExcelUrl = "/api/DumpENodebExcel";
    self.dumpCellExcelUrl = "/api/DumpCellExcel";
    self.dumpBtsExcelUrl = "/api/DumpBtsExcel";
    self.dumpCdmaCellExcelUrl = "/api/DumpCdmaCellExcel";

    // Route operations

    // Other private operations

    // Operations
    self.loadAlarms = function (id, viewModel) {
        sendRequest("/api/Alarms/", "GET", {
            eNodebId: id,
            begin: viewModel.beginDate(),
            end: viewModel.endDate()
        }, function (result) {
            viewModel.alarms(result);
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
