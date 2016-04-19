function Column3d() {
    var self = {};
    self.title = {
        text: '3D chart with null values'
    };
    self.xAxis = {
        categories: []
    };
    self.series = [{
        name: 'Sales',
        data: []
    }];
    self.tooltip = {
        headerFormat: '<span style="font-size:11px">{point.x}</span><br>',
        pointFormat: '<b>{point.y:.2f}</b>'
    };
    self.legend = {
        enabled: false
    };

    self.options = {
        chart: {
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 10,
                beta: 25,
                depth: 70
            }
        },
        title: self.title,
        plotOptions: {
            column: {
                depth: 25
            }
        },
        xAxis: self.xAxis,
        yAxis: {
            title: {
                text: null
            }
        },
        tooltip: self.tooltip,
        legend: self.legend,
        series: self.series
    };
    return self;
}