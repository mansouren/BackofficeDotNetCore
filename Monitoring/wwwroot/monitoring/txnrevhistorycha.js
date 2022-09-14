/*import { HubConnectionState } from "../microsoft/signalr/dist/esm";*/


let txnRevHisChart = {};
$(document).ready(function () {

    let isFirstCallSuccess = false;

    connection.on("txnRevHis", (result) => {
        if (!isFirstCallSuccess)
            gettxnrevhistorychart(JSON.parse(result));
    });

    connection.on("txnRevHisaddtxns", (result) => {
        addtxnrevhistorychart(JSON.parse(result));
    });


    function gettxnrevhistorychart(result) {
        console.log('Im getting data');
        var dataSets = new Array();
        for (i = 0; i < result.datasets.length; i++) {
            var entity = {};
            entity.name = result.datasets[i].label;
            entity.data = result.datasets[i].data;

            entity.type = 'area';
            dataSets.push(entity);

        }

        for (var i in result.datasets) {
            // result.datasets[i].pointBorderColor = "#fff";
            switch (parseInt(result.datasets[i].key)) {
                case 15:
                    result.datasets[i].backgroundColor = "rgba(48,128,20,0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(48,128,20,1)";
                    result.datasets[i].borderColor = "rgba(48,128,20,1)";
                    break;
                case 18:
                    result.datasets[i].backgroundColor = "rgba(0,102,204,0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(0,102,204,1)";
                    result.datasets[i].borderColor = "rgba(0,102,204,1)";
                    break;
                case 21:
                    result.datasets[i].backgroundColor = "rgba(108, 255, 241, 0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(108, 255, 241, 1)";
                    result.datasets[i].borderColor = "rgba(108, 255, 241, 1)";
                    break;
                case 23:
                    result.datasets[i].backgroundColor = "rgba(255,99,0,0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(255,99,0,1)";
                    result.datasets[i].borderColor = "rgba(255,99,0,1)";
                    break;
                case 26:
                    result.datasets[i].backgroundColor = "rgba(192,192,192,0.3)";
                    result.datasets[i].pointBackgroundColor = "rgba(192,192,192,1)";
                    result.datasets[i].borderColor = "rgba(192,192,192,1)";
                    break;
                case 29:
                    result.datasets[i].backgroundColor = "rgba(192,192,100,0.3)";
                    result.datasets[i].pointBackgroundColor = "rgba(100,192,192,1)";
                    result.datasets[i].borderColor = "rgba(192,192,100,1)";
                    break;
                case 30:
                    result.datasets[i].backgroundColor = "rgba(192,192,50,0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(192,192,50,1)";
                    result.datasets[i].borderColor = "rgba(192,50,192,1)";
                    break;
                case 31:
                    result.datasets[i].backgroundColor = "rgba(192,192,80,0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(80,80,192,1)";
                    result.datasets[i].borderColor = "rgba(192,80,80,1)";
                    break;
                case -1:
                    result.datasets[i].backgroundColor = "rgba(255, 51, 51, 0.2)";
                    result.datasets[i].pointBackgroundColor = "rgba(255,51,51,1)";
                    result.datasets[i].borderColor = "rgba(255,51,51,1)";
                    break;
            }
        }

        txnRevHisChart = Highcharts.chart('txnrevhischa', {
            chart: {
                zoomType: 'x',
                alignTicks: false,
                height: (10 / 22 * 100) + '%' // set ratio
            },

            //rangeSelector: {
            //    selected: 2,
            //    inputEnabled: false,
            //    buttons: [
            //        {
            //            type: 'all',
            //            count: 1,
            //            text: '5 ساعت'
            //        },
            //        {
            //            type: 'hour',
            //            count: 3,
            //            text: '3 ساعت'
            //        },
            //        {
            //            type: 'hour',
            //            count: 1,
            //            text: 'ساعت 1'
            //        }
            //    ],
            //    buttonTheme: {
            //        width: 100,
            //        useHTML: true,
            //        style: {
            //            fontSize: '15px',
            //            align: 'center',

            //        }

            //    },
            //    labelStyle: {
            //        visibility: 'hidden'
            //    }
            //},
            title: {
                text: 'نمودار تراکنش معکوس',
                style: {
                    fontFamily: 'IRANSansWeb'
                },
            },
            credits: {
                text: ''
            },
            legend: {
                enabled: true,
                rtl: true,
                y: 25,
                margin: 0,
                shadow: true,
                verticalAlign: 'top',
                itemStyle: {
                    cursor: "pointer",
                    fontFamily: 'IRANSansWeb_Light',
                }
            },
            tooltip: {

                enabled: true,
                rtl: false,
                split: false,
                shared: true,
                crosshairs: true,
                xDateFormat: '%H:%M ',
                useHTML: true,
                style: {
                    fontFamily: 'IRANSansWeb',
                    fontSize: '15px',
                    rtl: false,
                },
                distance: 30,
                padding: 5,
                headerFormat: '<span>{point.key}</span><br/>',
                formatter: function () {
                    return this.points.reduce(function (s, point) {
                        return s + '<br/>' +
                            "<span style='color:" + point.series.color + ";'>\u25CF </span> " +
                            point.series.name + ': ' +
                            point.y;
                    }, '<b>' + getTimeFormat(this.x) + '</b>');
                }
            },
            xAxis: {
                zoomEnabled: true,
                categories: result.labels,
                type: 'datetime',
                tickInterval: 1,
                tickPixelInterval: 100,
                tickmarkPlacement: 'on',
                startOnTick: true,
                endOnTick: true,
                labels: {
                    formatter() {

                        return getTimeFormat(this.value);
                    },
                    rotation: 55,
                    step: 1,
                    style: {
                        fontSize: '11px',
                        fontFamily: 'IRANSansWeb'
                    }
                }
            },
            time: {
                timezoneOffset: new Date().getTimezoneOffset()
            },

            yAxis: {
                title: {
                    text: "تعداد",
                    style: {
                        fontSize: '15px',
                        fontFamily: 'IRANSansWeb'
                    },
                },
                opposite: false,
                endOnTick: true,
                showLastLabel: true,
                //max: chart.yAxis[0].max,
                labels: {
                    style: {
                        fontSize: '15px',
                        fontFamily: 'IRANSansWeb'
                    },
                    align: 'right',
                    x: -7,
                    y: 5,
                    formatter: function () {
                        return this.value;
                    }
                }
            },
            plotOptions: {
                series: {
                    pointRange: 1 * 60 * 1000,
                    fillOpacity: 0.2,
                    marker: {
                        symbol: 'circle',
                        //lineColor: 'white',
                        //lineWidth: 4
                    }
                }
            },
            series: dataSets,
            navigator: {
                enabled: true
            },
            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            enabled: false
                        }
                    }
                }]
            }

        });

        isFirstCallSuccess = true;
    }

    function addtxnrevhistorychart(result) {

        if (!isFirstCallSuccess) return;

        if (txnRevHisChart.xAxis[0].categories[txnRevHisChart.xAxis[0].categories.length - 1] != result.labels[0]) {

            txnRevHisChart.xAxis[0].categories.push(result.labels[0]);
            txnRevHisChart.series[0].addPoint(result.datasets[0], true, true);
            txnRevHisChart.series[1].addPoint(result.datasets[1], true, true);
            txnRevHisChart.series[2].addPoint(result.datasets[2], true, true);
            txnRevHisChart.series[3].addPoint(result.datasets[3], true, true);
            txnRevHisChart.series[4].addPoint(result.datasets[4], true, true);
            txnRevHisChart.series[5].addPoint(result.datasets[5], true, true);
            txnRevHisChart.series[6].addPoint(result.datasets[6], true, true);
            txnRevHisChart.series[7].addPoint(result.datasets[7], true, true);
            txnRevHisChart.series[8].addPoint(result.datasets[8], true, true);

        }
    }

});









