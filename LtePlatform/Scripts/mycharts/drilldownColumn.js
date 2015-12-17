var DrilldownColumn = function() {

};

DrilldownColumn.prototype = new DrilldownChart();

DrilldownColumn.prototype.options.chart = {
    type: 'column'
};

DrilldownColumn.prototype.options.plotOptions.series.dataLabels.format = '{point.y}';

DrilldownColumn.prototype.options.xAxis = {
    type: 'category'
};