let txnresphisbycha = {};
$(document).ready(function () {
    let isFirstCallSuccess = false;

    connection.on("populatetxns", (result) => {
        if (!isFirstCallSuccess)
            getChartData(JSON.parse(result));
    });
    connection.on("populateaddtxns", (result) => {
        addChartData(JSON.parse(result));
    });

    var dataSets = new Array();

    function getChartData(result) {

        console.log('Im getting data');

        for (i = 0; i < result.datasets.length; i++) {
            var entity = {};
            entity.name = result.datasets[i].label;
            entity.data = result.datasets[i].data;
            entity.color = result.datasets[i].color;
            entity.type = 'area';
            dataSets.push(entity);
            debugger;
        }

        txnresphisbycha = Highcharts.chart('txnhisbyrespcha', {
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
                text: 'نمودار تراکنش کل',
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
                xDateFormat: '%H:%M',
                useHTML: true,
                style: {
                    fontFamily: 'IRANSansWeb',
                    fontSize: '15px',

                },
                distance: 30,
                padding: 5,
                headerFormat: '<span>{point.key}</span><br/>',
                formatter: function () {
                    return this.points.reduce(function (s, point) {
                        return s + '<br/>' +
                            point.series.name + ': ' +
                            point.y + "<span style='color:" + point.series.color + ";'>\u25CF </span> ";
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

    function addChartData(result) {

        if (!isFirstCallSuccess) return;
        if (txnresphisbycha.xAxis[0].categories[txnresphisbycha.xAxis[0].categories.length - 1] != result.labels[0]) {

            txnresphisbycha.xAxis[0].categories.push(result.labels[0]);
            txnresphisbycha.series[0].addPoint(result.datasets[0].data[0], true, true);
            txnresphisbycha.series[1].addPoint(result.datasets[0].data[1], true, true);
            txnresphisbycha.series[2].addPoint(result.datasets[0].data[2], true, true);
            txnresphisbycha.series[3].addPoint(result.datasets[0].data[3], true, true);

        }



    }


})
