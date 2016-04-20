function GradientPie() {
    var self = {};
    self.chart = {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    };
    self.title = {
        text: 'Browser market shares. January, 2015 to May, 2015'
    };
    self.tooltip = {
        pointFormat: '{series.name}: <b>{point.y}, 占比{point.percentage:.1f}%</b>'
    };
    self.plotOptions = {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                },
                connectorColor: 'silver'
            }
        }
    };
    self.series = [
        {
            name: 'Brands',
            data: []
        }
    ];

    self.options = {
        chart: self.chart,
        title: self.title,
        tooltip: self.tooltip,
        plotOptions: self.plotOptions,
        series: self.series
    };

    return self;
}

function GaugeMeter() {
    var self = {};
    self.chart = {
        type: 'gauge',
        plotBackgroundColor: null,
        plotBackgroundImage: null,
        plotBorderWidth: 0,
        plotShadow: false
    };
    self.title = {
        text: 'Speedometer'
    };
    self.pane = {
        startAngle: -150,
        endAngle: 150,
        background: [{
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#FFF'],
                    [1, '#333']
                ]
            },
            borderWidth: 0,
            outerRadius: '109%'
        }, {
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#333'],
                    [1, '#FFF']
                ]
            },
            borderWidth: 1,
            outerRadius: '107%'
        }, {
            // default background
        }, {
            backgroundColor: '#DDD',
            borderWidth: 0,
            outerRadius: '105%',
            innerRadius: '103%'
        }]
    };
    self.yAxis = {
        min: 0,
        max: 200,

        minorTickInterval: 'auto',
        minorTickWidth: 1,
        minorTickLength: 10,
        minorTickPosition: 'inside',
        minorTickColor: '#666',

        tickPixelInterval: 30,
        tickWidth: 2,
        tickPosition: 'inside',
        tickLength: 10,
        tickColor: '#666',
        labels: {
            step: 2,
            rotation: 'auto'
        },
        title: {
            text: 'km/h'
        },
        plotBands: [{
            from: 0,
            to: 120,
            color: '#DF5353' // red
        }, {
            from: 120,
            to: 160,
            color: '#DDDF0D' // yellow
        }, {
            from: 160,
            to: 200,
            color: '#55BF3B' // green
        }]
    };
    self.series = [{
        name: 'Speed',
        data: [80]
    }];
    self.options = {
        chart: self.chart,
        title: self.title,
        pane: self.pane,
        yAxis: self.yAxis,
        series: self.series
    };
    return self;
}