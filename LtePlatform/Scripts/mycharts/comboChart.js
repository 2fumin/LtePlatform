function ComboChart() {
    var self = this;
    self.title = {
        text: 'The Combo Chart including line, column, pie, ...'
    };

    self.yAxis = [{ 
        labels: {
            format: '{value}',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        },
        title: {
            text: 'YLabel',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        }
    }];

    self.xAxis= [{
        categories: [],
        title: {
            text: 'xLabel',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        }
    }];

    self.series = [];

    self.legend = {
        layout: 'vertical',
        align: 'left',
        x: 100,
        verticalAlign: 'top',
        y: 30,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
    };
    
    self.tooltip = {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<b>{point.y:.2f}</b>'
    };

    self.options = {
        chart: {
            zoomType: 'xy'
        },
        title: self.title,
        xAxis: self.xAxis,
        yAxis: self.yAxis,
        tooltip: self.tooltip,
        legend: self.legend,
        series: self.series
    };
}

function BarChart() {
    var self = this;
    self.title = {
        text: 'Historic World Population by Region'
    };
    self.xAxis = {
        categories: [],
        title: {
            text: null
        }
    };
    self.yAxis = {
        min: 0,
        title: {
            text: 'Population (millions)',
            align: 'high'
        },
        labels: {
            overflow: 'justify'
        }
    };
    self.tooltip = {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<b>{point.y:.2f}</b>'
    };
    self.plotOptions = {
        bar: {
            dataLabels: {
                enabled: true,
                align: "center",
                color: 'red',
                formatter: function() {
                    return parseInt(this.y * 100) / 100;
                }
            }
        }
    };
    self.legend = {
        enabled: false
    };
    self.series = [];
    self.options = {
        chart: {
            type: 'bar'
        },
        title: self.title,
        xAxis: self.xAxis,
        yAxis: self.yAxis,
        tooltip: self.tooltip,
        plotOptions: self.plotOptions,
        legend: self.legend,
        credits: {
            enabled: false
        },
        series: self.series
    };
}