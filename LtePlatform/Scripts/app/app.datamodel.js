function AppDataModel() {
    var self = this;
    // Routes
    self.userInfoUrl = "/api/Me";
    self.siteUrl = "/";
    self.applicationUsersUrl = "/api/ApplicationUsers";
    self.applicationRolesUrl = "/api/ApplicationRoles";

    self.cityListUrl = "/api/CityList";
    self.kpiDataListUrl = "/api/KpiDataList";
    self.preciseStatUrl = "/api/PreciseStat/";
    self.preciseRegionUrl = "/api/PreciseRegion/";
    self.alarmCountUrl = "/api/AlarmCount";
    self.dumpAlarmUrl = "/api/DumpAlarm";
    self.dumpNeighborUrl = "/api/DumpNeighbor";
    self.dumpInterferenceUrl = "/api/DumpInterference";
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
    self.sectorViewUrl = "/api/SectorView";
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
    self.workItemUrl = "/api/WorkItem";
    self.lteNeighborCellUrl = "/api/LteNeighborCell";
    self.nearestPciCellUrl = "/api/NearestPciCell";
    self.neighborMonitorUrl = "/api/NeighborMonitor";
    self.interferenceNeighborUrl = "/api/InterferenceNeighbor";
    self.interferenceVictimUrl = "/api/InterferenceVictim";

    self.areaTestDateUrl = "/api/AreaTestDate";
    self.csvFileInfoUrl = "/api/CsvFileInfo";
    self.rasterInfoUrl = "/api/RasterInfo";
    self.rasterFileUrl = "/api/RasterFile";
    self.record2GUrl = "/api/Record2G";
    self.record3GUrl = "/api/Record3G";
    self.record4GUrl = "/api/Record4G";

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

    self.initializeAuthorization = function() {
        if (!self.getAccessToken()) {
            // The following code looks for a fragment in the URL to get the access token which will be
            // used to call the protected Web API resource
            var fragment = common.getFragment();

            if (fragment.access_token) {
                // returning with access token, restore old hash, or at least hide token
                window.location.hash = fragment.state || '';
                self.setAccessToken(fragment.access_token);
            } else {
                // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
            }
        }
    };
}
