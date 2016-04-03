var app = angular.module('myApp', [
    'college',
    'college.map',
    'handoff.parameters',
    "highcharts-ng",
    'huawei.mongo.parameters',
    'kpi.basic',
    'kpi.import',
    'kpi.workitem',
    'myApp.dumpInterference',
    'myApp.kpi',
    'myApp.parameters',
    'myApp.region',
    'myApp.url',
    'ne.geometry',
    'neighbor.mongo',
    'ngRoute',
    'parametersMap',
    'parameters.chart',
    "ui.bootstrap",
    'ui.router'
]);

app.run(function($rootScope) {
    $rootScope.sideBarShown = true;
});