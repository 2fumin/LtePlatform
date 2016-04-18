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
    Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
        return {
            radialGradient: {
                cx: 0.5,
                cy: 0.3,
                r: 0.7
            },
            stops: [
                [0, color],
                [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
            ]
        };
    });
});