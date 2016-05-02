app.factory('showPieChart', function() {
    return {
        type: function(views, tag) {
            var stats = [];
            var i;
            for (i = 0; i < views.length; i++) {
                var type = views[i].workItemType;
                var subType = views[i].workItemSubType;
                var j;
                for (j = 0; j < stats.length; j++) {
                    if (stats[j].type === type) {
                        stats[j].total++;
                        var subData = stats[j].subData;
                        var k;
                        for (k = 0; k < subData.length; k++) {
                            if (subData[k][0] === subType) {
                                subData[k][1]++;
                                break;
                            }
                        }
                        if (k === subData.length) {
                            subData.push([subType, 1]);
                        }
                        break;
                    }
                }
                if (j === stats.length) {
                    stats.push({
                        type: type,
                        total: 1,
                        subData: [[subType, 1]]
                    });
                }
            }

            var chart = new DrilldownPie();
            chart.title.text = "工单类型分布图";
            chart.series[0].data = [];
            chart.drilldown.series = [];
            chart.series[0].name = "工单类型";
            for (i = 0; i < stats.length; i++) {
                chart.addOneSeries(stats[i].type, stats[i].total, stats[i].subData);
            }
            $(tag).highcharts(chart.options);
        },

        state: function(views, tag) {
            var stats = [];
            var i;
            for (i = 0; i < views.length; i++) {
                var state = views[i].workItemState;
                var subType = views[i].workItemSubType;
                var j;
                for (j = 0; j < stats.length; j++) {
                    if (stats[j].state === state) {
                        stats[j].total++;
                        var subData = stats[j].subData;
                        var k;
                        for (k = 0; k < subData.length; k++) {
                            if (subData[k][0] === subType) {
                                subData[k][1]++;
                                break;
                            }
                        }
                        if (k === subData.length) {
                            subData.push([subType, 1]);
                        }
                        break;
                    }
                }
                if (j === stats.length) {
                    stats.push({
                        state: state,
                        total: 1,
                        subData: [[subType, 1]]
                    });
                }
            }

            var chart = new DrilldownPie();
            chart.title.text = "工单状态分布图";
            chart.series[0].data = [];
            chart.drilldown.series = [];
            chart.series[0].name = "工单状态";
            for (i = 0; i < stats.length; i++) {
                chart.addOneSeries(stats[i].state, stats[i].total, stats[i].subData);
            }
            $(tag).highcharts(chart.options);
        }
    }
})
.factory('workItemDialog', function ($uibModal, $log, workitemService) {
    return {
        feedback: function (view, callbackFunc) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/WorkItem/FeedbackDialog.html',
                controller: 'workitem.feedback.dialog',
                size: 'lg',
                resolve: {
                    dialogTitle: function () {
                        return view.serialNumber + "工单反馈";
                    },
                    input: function () {
                        return view;
                    }
                }
            });

            modalInstance.result.then(function (output) {
                workitemService.feedback(output, view.serialNumber).then(function (result) {
                    if (result)
                        callbackFunc();
                });
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        },
        showDetails: function(view) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/WorkItem/DetailsDialog.html',
                controller: 'workitem.details.dialog',
                size: 'lg',
                resolve: {
                    dialogTitle: function () {
                        return view.serialNumber + "工单信息";
                    },
                    input: function () {
                        return view;
                    }
                }
            });

            modalInstance.result.then(function (output) {
                console.log(output);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        },
        calculatePlatformInfo: function (comments) {
            var platformInfos = [];
            if (comments) {
                var fields = comments.split('[');
                if (fields.length > 1) {
                    angular.forEach(fields, function(field) {
                        var subFields = field.split(']');
                        platformInfos.push({
                            time: subFields[0],
                            contents: subFields[1]
                        });
                    });
                }
            }

            return platformInfos;
        }
    };
});
