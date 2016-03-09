app.controller("parameters.list", function($scope, $http) {
    $scope.chartConfig = {

        options: {
            //This is the Main Highcharts chart config. Any Highchart options are valid here.
            //will be overriden by values specified below.
            chart: {
                type: 'column'
            },
            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<b>{point.y:.2f}</b>'
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                x: 100,
                verticalAlign: 'top',
                y: 30,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
            }
        },
        //The below properties are watched separately for changes.

        //Series object (optional) - a list of series using normal highcharts series options.
        series: [{
            data: [10, 15, 12, 8, 7],
            name: 'A useful series.'
        }],
        //Title configuration (optional)
        title: {
            text: 'Hello'
        },
        //Boolean to control showng loading status on chart (optional)
        //Could be a string if you want to show specific loading text.
        loading: false,
        //Configuration for the xAxis (optional). Currently only one x axis can be dynamically controlled.
        //properties currentMin and currentMax provied 2-way binding to the chart's maximimum and minimum
        xAxis: {
            categories: [2, 4, 6, 8, 12],
            title: {
                text: 'This is the x label!',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            }
        },
        yAxis: [{ 
            labels: {
                format: '{value}',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            title: {
                text: 'This is the Y label.',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            }
        }],
        //Whether to use HighStocks instead of HighCharts (optional). Defaults to false.
        useHighStocks: false,
        //function (optional)
        func: function (chart) {
            //setup some logic for the chart
        }
    };
});