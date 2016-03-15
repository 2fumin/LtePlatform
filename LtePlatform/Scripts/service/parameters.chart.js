angular.module('parameters.chart', [])
    .factory('parametersChartService', function() {
        return {
            getDistrictLteENodebPieOptions: function(data, city) {
                var chart = new GradientPie();
                chart.title.text = city + "各区LTE基站数分布";
                chart.series[0].name = "区域";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].district,
                        y: data[i].totalLteENodebs
                    });
                }
                return chart.options;
            },
            getDistrictLteCellPieOptions: function(data, city) {
                var chart = new GradientPie();
                chart.title.text = city + "各区LTE小区数分布";
                chart.series[0].name = "区域";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].district,
                        y: data[i].totalLteCells
                    });
                }
                return chart.options;
            },
            getDistrictCdmaBtsPieOptions: function (data, city) {
                var chart = new GradientPie();
                chart.title.text = city + "各区CDMA基站数分布";
                chart.series[0].name = "区域";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].district,
                        y: data[i].totalCdmaBts
                    });
                }
                return chart.options;
            },
            getDistrictCdmaCellPieOptions: function (data, city) {
                var chart = new GradientPie();
                chart.title.text = city + "各区CDMA小区数分布";
                chart.series[0].name = "区域";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].district,
                        y: data[i].totalCdmaCells
                    });
                }
                return chart.options;
            },
            getTownLteENodebPieOptions: function (data, district) {
                var chart = new GradientPie();
                chart.title.text = district + "各镇LTE基站数分布";
                chart.series[0].name = "镇";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].town,
                        y: data[i].totalLteENodebs
                    });
                }
                return chart.options;
            },
            getTownLteCellPieOptions: function (data, district) {
                var chart = new GradientPie();
                chart.title.text = district + "各镇LTE小区数分布";
                chart.series[0].name = "镇";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].town,
                        y: data[i].totalLteCells
                    });
                }
                return chart.options;
            },
            getTownCdmaBtsPieOptions: function (data, district) {
                var chart = new GradientPie();
                chart.title.text = district + "各镇CDMA基站数分布";
                chart.series[0].name = "镇";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].town,
                        y: data[i].totalCdmaBts
                    });
                }
                return chart.options;
            },
            getTownCdmaCellPieOptions: function (data, district) {
                var chart = new GradientPie();
                chart.title.text = district + "各镇CDMA小区数分布";
                chart.series[0].name = "镇";
                for (var i = 0; i < data.length; i++) {
                    chart.series[0].data.push({
                        name: data[i].town,
                        y: data[i].totalCdmaCells
                    });
                }
                return chart.options;
            }
        };
    });