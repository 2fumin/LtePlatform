using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace LtePlatform
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-bootstraps").Include(
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/uib/accordion.js",
                "~/Scripts/angular-ui/uib/alert.js",
                "~/Scripts/angular-ui/uib/buttons.js",
                "~/Scripts/angular-ui/uib/carousel.js",
                "~/Scripts/angular-ui/uib/collapse.js",
                "~/Scripts/angular-ui/uib/dateparser.js",
                "~/Scripts/angular-ui/uib/datepicker.js",
                "~/Scripts/angular-ui/uib/debounce.js",
                "~/Scripts/angular-ui/uib/dropdown.js",
                "~/Scripts/angular-ui/uib/isClass.js",
                "~/Scripts/angular-ui/uib/modal.js",
                "~/Scripts/angular-ui/uib/pager.js",
                "~/Scripts/angular-ui/uib/pagination.js",
                "~/Scripts/angular-ui/uib/paging.js",
                "~/Scripts/angular-ui/uib/popover.js",
                "~/Scripts/angular-ui/uib/position.js",
                "~/Scripts/angular-ui/uib/progressbar.js",
                "~/Scripts/angular-ui/uib/rating.js",
                "~/Scripts/angular-ui/uib/stackedMap.js",
                "~/Scripts/angular-ui/uib/tooltip.js",
                "~/Scripts/angular-ui/uib/tabs.js",
                "~/Scripts/angular-ui/uib/timepicker.js",
                "~/Scripts/angular-ui/uib/typeahead.js"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                "~/Scripts/Highcharts/highcharts.src.js",
                "~/Scripts/Highcharts/highcharts-3d.js",
                "~/Scripts/Highcharts/highcharts-more.js",
                "~/Scripts/Highcharts/modules/exporting.js",
                "~/Scripts/Highcharts/modules/data.js",
                "~/Scripts/Highcharts/modules/drilldown.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/dtList").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/dt/list.viewmodel.js",
                "~/Scripts/app/_run.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/collegeMap").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/mycharts/drilldown.chart.js",
                "~/Scripts/mycharts/drilldownColumn.js",
                "~/Scripts/baidu/plugins/baidu.tangram.js",
                "~/Scripts/baidu/plugins/InfoBox.js",
                "~/Scripts/baidu/mapContainer.js",
                "~/Scripts/baidu/cloud/geography.map.js",
                "~/Scripts/baidu/college.helper.js",
                "~/Scripts/baidu/parameters.helper.js",
                "~/Scripts/mycharts/comboChart.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/map.viewmodel.js",
                "~/Scripts/college/map.controller.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/collegeCoverage").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/baidu/plugins/baidu.tangram.js",
                "~/Scripts/baidu/plugins/InfoBox.js",
                "~/Scripts/baidu/mapContainer.js",
                "~/Scripts/baidu/dtgenerator.js",
                "~/Scripts/baidu/cloud/geography.map.js",
                "~/Scripts/baidu/college.helper.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/map.controller.js",
                "~/Scripts/college/coverage.viewmodel.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/collegeTest").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/common.controller.js",
                "~/Scripts/college/test.viewmodel.js",
                "~/Scripts/college/test.controller.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/collegeKpi").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/common.controller.js",
                "~/Scripts/college/kpi.viewmodel.js",
                "~/Scripts/college/kpi.controller.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/collegeInfrastructure").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/infrastructure.viewmodel.js",
                "~/Scripts/app/_run.js"));

            bundles.Add(new ScriptBundle("~/bundles/collegePrecise").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/mycharts/comboChart.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/college/common.controller.js",
                "~/Scripts/college/precise.viewmodel.js",
                "~/Scripts/college/precise.controller.js",
                "~/Scripts/app/_run.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/parametersAlarmImport").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/parameters/common.controller.js",
                "~/Scripts/kpi/common.controller.js",
                "~/Scripts/parameters/alarmimport.controller.js",
                "~/Scripts/parameters/alarmimport.viewmodel.js",
                "~/Scripts/app/_run.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/webapiBasicPost").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Areas/TestPage/Scripts/basicpost.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap-theme.css",
                "~/Content/themes/ui-bootstrap.css",
                 "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/all.css"));

            bundles.Add(new StyleBundle("~/Content/themes/cloudmap/css").Include(
                "~/Content/themes/map/all.css"));

            bundles.Add(new StyleBundle("~/Content/HelpPage").Include(
                "~/Areas/HelpPage/HelpPage.css"));
        }
    }
}
