function comboChart() {
    var self = this;
    self.title = {
        text: 'The Combo Chart including line, column, pie, ...'
    };
    self.categories = ['Apples', 'Oranges', 'Pears', 'Bananas', 'Plums'];

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
        categories: self.categories,
        labels: {
            step: 1
        },
        title: {
            text: 'xLabel',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        }
    }];

    self.series = [{
        type: 'column',
        name: 'Jane',
        data: [3, 2, 1, 3, 4]
    }, {
        type: 'column',
        name: 'John',
        data: [2, 3, 5, 7, 6]
    }, {
        type: 'column',
        name: 'Joe',
        data: [4, 3, 3, 9, 0]
    }, {
        type: 'spline',
        name: 'Average',
        data: [3, 2.67, 3, 6.33, 3.33],
        marker: {
            lineWidth: 2,
            lineColor: Highcharts.getOptions().colors[3],
            fillColor: 'white'
        }
    }];

    self.legend = {
        layout: 'vertical',
        align: 'left',
        x: 100,
        verticalAlign: 'top',
        y: 30,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
    };
    self.xLabel = 'XLabel';

    self.options = {
        chart: {
            zoomType: 'xy'
        },
        title: self.title,
        xAxis: self.xAxis,
        yAxis: self.yAxis,
        tooltip: {
            shared: true
        },
        legend: self.legend,
        series: self.series
    };
}