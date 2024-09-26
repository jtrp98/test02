function renderChart1(Result) {
    $.each(Result, function (index) {
        var max = Number(Result[index].grade40);
        if (Number(Result[index].grade35) > max) max = Result[index].grade35;
        if (Number(Result[index].grade30) > max) max = Result[index].grade30;
        if (Number(Result[index].grade25) > max) max = Result[index].grade25;
        if (Number(Result[index].grade20) > max) max = Result[index].grade20;
        if (Number(Result[index].grade15) > max) max = Result[index].grade15;
        if (Number(Result[index].grade10) > max) max = Result[index].grade10;
        if (Number(Result[index].grade00) > max) max = Result[index].grade00;

        Chart.defaults.global.legend.display = false;
        var options = {
            type: 'bar',
            data: {
                labels: [4, 3.5, 3, 2.5, 2, 1.5, 1, 0],
                datasets: [
                    {
                        data: [Result[index].grade40, Result[index].grade35, Result[index].grade30, Result[index].grade25, Result[index].grade20, Result[index].grade15, Result[index].grade10, Result[index].grade00],
                        fill: 'origin',
                        pointStyle: 'dash',
                        borderWidth: 1,
                        backgroundColor: 'rgba(255,192,192,1)'
                    }
                ]
            },
            options: {
                elements: {
                    line: {
                        fill: '-1'
                    }
                },

                "animation": {
                    "duration": 1,
                    "onComplete": function () {
                        var chartInstance = this.chart,
                            ctx = chartInstance.ctx;

                        ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'bottom';

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var data = dataset.data[index];
                                ctx.fillText(data, bar._model.x, bar._model.y - 5);
                            });
                        });
                    }
                },
                scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'จำนวนนักเรียน'
                        },
                        ticks: {
                            suggestedMax: Number(max) + 2
                        }
                    }],

                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'เกรด'
                        }
                    }]
                },
                title: {
                    display: true,
                    text: [Result[index].header]
                }
            }
        }
        var ctx = document.getElementById('myChart').getContext('2d');
        new Chart(ctx, options);
    });
}

function renderChart2(Result) {
    $.each(Result, function (index) {
        var max = Number(Result[index].b3);
        if (Number(Result[index].b2) > max) max = Result[index].b2;
        if (Number(Result[index].b1) > max) max = Result[index].b1;
        if (Number(Result[index].b0) > max) max = Result[index].b0;

        Chart.defaults.global.legend.display = false;
        var options = {
            type: 'bar',
            data: {
                labels: [3, 2, 1, 0],
                datasets: [
                    {
                        data: [Result[index].b3, Result[index].b2, Result[index].b1, Result[index].b0],
                        fill: 'origin',
                        pointStyle: 'dash',
                        borderWidth: 1,
                        backgroundColor: 'rgba(255,192,192,1)'
                    }
                ]
            },
            options: {
                elements: {
                    line: {
                        fill: '-1'
                    }
                },

                "animation": {
                    "duration": 1,
                    "onComplete": function () {
                        var chartInstance = this.chart,
                            ctx = chartInstance.ctx;

                        ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'bottom';

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var data = dataset.data[index];
                                ctx.fillText(data, bar._model.x, bar._model.y - 5);
                            });
                        });
                    }
                },
                scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'จำนวนนักเรียน'
                        },
                        ticks: {
                            suggestedMax: Number(max) + 1,
                            stepSize: 1
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'คะแนนคุณลักษณะอันพึงประสงค์'
                        }
                    }]
                },
                title: {
                    display: true,
                    text: [Result[index].header2]
                }
            }
        }

        var ctx = document.getElementById('myChart2').getContext('2d');
        new Chart(ctx, options);

    });
}

function renderChart3(Result) {
    $.each(Result, function (index) {
        var max = Number(Result[index].r3);
        if (Number(Result[index].r2) > max) max = Result[index].r2;
        if (Number(Result[index].r1) > max) max = Result[index].r1;
        if (Number(Result[index].r0) > max) max = Result[index].r0;

        Chart.defaults.global.legend.display = false;
        var options = {
            type: 'bar',
            data: {
                labels: [3, 2, 1, 0],
                datasets: [
                    {
                        data: [Result[index].r3, Result[index].r2, Result[index].r1, Result[index].r0],
                        fill: 'origin',
                        pointStyle: 'dash',
                        borderWidth: 1,
                        backgroundColor: 'rgba(255,192,192,1)'
                    }
                ]
            },
            options: {
                elements: {
                    line: {
                        fill: '-1'
                    }
                },

                "animation": {
                    "duration": 1,
                    "onComplete": function () {
                        var chartInstance = this.chart,
                            ctx = chartInstance.ctx;

                        ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                        ctx.textAlign = 'center';
                        ctx.textBaseline = 'bottom';

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var data = dataset.data[index];
                                ctx.fillText(data, bar._model.x, bar._model.y - 5);
                            });
                        });
                    }
                },
                scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'จำนวนนักเรียน'
                        },
                        ticks: {
                            suggestedMax: Number(max) + 1,
                            stepSize: 1
                        }
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'คะแนนการอ่าน คิด วิเคราะห์และเขียน'
                        }
                    }]
                },
                title: {
                    display: true,
                    text: [Result[index].header3]
                }
            }
        };

        var ctx = document.getElementById('myChart3').getContext('2d');
        new Chart(ctx, options);
    });
}