var showMrPie = function(districtStats, townStats) {
    var chart = new DrilldownPie();
    chart.title.text = "分镇区测量报告数分布图";
    chart.series[0].data = [];
    chart.drilldown.series = [];
    chart.series[0].name = "区域";
    for (var i = 0; i < districtStats.length; i++) {
        var subData = [];
        var district = districtStats[i].district;
        var districtMr = districtStats[i].totalMrs;
        for (var j = 0; j < townStats.length; j++) {
            if (townStats[j].district === district) {
                subData.push([townStats[j].town, townStats[j].totalMrs]);
            }
        }
        chart.addOneSeries(district, districtMr, subData);
    }
    $("#mr-pie").highcharts(chart.options);
};