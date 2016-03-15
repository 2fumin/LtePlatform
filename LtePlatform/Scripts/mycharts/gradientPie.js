function GradientPie() {
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