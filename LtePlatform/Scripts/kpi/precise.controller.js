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

var generateDistrictStats = function (viewModel, result) {
    for (var i = 0; i < result.length; i++) {
        var districtViews = result[i].districtPreciseViews;
        var statDate = result[i].statDate;
        var totalMrs = 0;
        var totalSecondNeighbors = 0;
        var districtMrStats = [];
        var districtPreciseRates = [];
        for (var j = 0; j < viewModel.districts().length - 1; j++) {
            var currentDistrictMrs = 0;
            var currentPreciseRate = 0;
            for (var k = 0; k < districtViews.length; k++) {
                var view = districtViews[k];
                if (view.district === viewModel.districts()[j]) {
                    currentDistrictMrs = view.totalMrs;
                    currentPreciseRate = view.preciseRate;
                    totalMrs += currentDistrictMrs;
                    totalSecondNeighbors += view.secondNeighbors;
                    break;
                }
            }
            districtMrStats.push(currentDistrictMrs);
            districtPreciseRates.push(currentPreciseRate);
        }
        districtMrStats.push(totalMrs);
        districtPreciseRates.push(100 - 100 * totalSecondNeighbors / totalMrs);
        viewModel.mrStats.push({
            statDate: statDate,
            values: districtMrStats
        });
        viewModel.preciseStats.push({
            statDate: statDate,
            values: districtPreciseRates
        });
    }
};

var showMrsDistrictChart = function (viewModel, tag) {
    var chart = new ComboChart();
    chart.title.text = "MR总数变化趋势图";
    var statDates = [];
    var districtStats = [];
    for (var i = 0; i < viewModel.mrStats().length; i++) {
        var stat = viewModel.mrStats()[i];
        statDates.push(stat.statDate);
        for (var j = 0; j < viewModel.districts().length ; j++) {
            if (i == 0) {
                districtStats.push([stat.values[j]]);
            } else {
                districtStats[j].push(stat.values[j]);
            }
        }
    }
    chart.xAxis[0].categories = statDates;
    chart.yAxis[0].title.text = "MR总数";
    chart.xAxis[0].title.text = '日期';
    for (j = 0; j < viewModel.districts().length; j++) {
        chart.series.push({
            type: j === viewModel.districts().length - 1 ? "spline" : "column",
            name: viewModel.districts()[j],
            data: districtStats[j]
        });
    }
    $(tag).highcharts(chart.options);
};

var showPreciseDistrictChart = function (viewModel, tag) {
    var chart = new ComboChart();
    chart.title.text = "精确覆盖率变化趋势图";
    var statDates = [];
    var districtStats = [];
    for (var i = 0; i < viewModel.preciseStats().length; i++) {
        var stat = viewModel.preciseStats()[i];
        statDates.push(stat.statDate);
        for (var j = 0; j < viewModel.districts().length ; j++) {
            if (i == 0) {
                districtStats.push([stat.values[j]]);
            } else {
                districtStats[j].push(stat.values[j]);
            }
        }
    }
    chart.xAxis[0].categories = statDates;
    chart.yAxis[0].title.text = "精确覆盖率";
    chart.xAxis[0].title.text = '日期';
    for (j = 0; j < viewModel.districts().length; j++) {
        chart.series.push({
            type: j === viewModel.districts().length - 1 ? "spline" : "line",
            name: viewModel.districts()[j],
            data: districtStats[j]
        });
    }
    $(tag).highcharts(chart.options);
};

var accumulatePreciseStat = function (source, accumulate) {
    source.totalMrs += accumulate.totalMrs;
    source.firstNeighbors += accumulate.firstNeighbors;
    source.secondNeighbors += accumulate.secondNeighbors;
    source.thirdNeighbors += accumulate.thirdNeighbors;
};

var calculateDistrictRates = function (districtStat) {
    districtStat.firstRate = 100 - 100 * districtStat.firstNeighbors / districtStat.totalMrs;
    districtStat.preciseRate = 100 - 100 * districtStat.secondNeighbors / districtStat.totalMrs;
};

var calculateTownRates = function (townStat) {
    townStat.firstRate = 100 - 100 * townStat.firstNeighbors / townStat.totalMrs;
    townStat.preciseRate = 100 - 100 * townStat.secondNeighbors / townStat.totalMrs;
    townStat.thirdRate = 100 - 100 * townStat.thirdNeighbors / townStat.totalMrs;
}