let txnresphisbycha = {};
$(document).ready(function () {
    let isFirstCallSuccess = false;

    var connection = new signalR
        .HubConnectionBuilder()
        .withUrl("/monitoringHub").build();

    connection.start().then(() => console.log("hubconnected")).catch(() => console.log(error));//establish connection


    connection.on("populatetxns", (result) => {
        if (!isFirstCallSuccess)
            getChartData(JSON.parse(result));
    });
    connection.on("populateaddtxns", (result) => {
        addChartData(JSON.parse(result));
    });


    function getChartData(result) {

        var dataSets = new Array();

        for (i = 0; i < result.datasets.length; i++) {
            var entity = {};
            entity.name = result.datasets[i].label;
            entity.data = result.datasets[i].data;
            entity.color = result.datasets[i].color;
            entity.type = 'area';
            dataSets.push(entity);

        }

        txnresphisbycha = Highcharts.chart('txnrevhischa', {
            chart: {
                zoomType: 'x',
                alignTicks: false,
                height: (10 / 22 * 100) + '%' // set ratio
            },
            global: {
                useUTC: false
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
                rtl: true,
                split: false,
                shared: true,
                xDateFormat: '%H:%M',
                useHTML: true,
                style: {
                    fontFamily: 'IRANSansWeb',
                    fontSize: '15px',
                    rtl: true,
                },
                distance: 30,
                padding: 5,
                headerFormat: '<span>{point.key}</span><br/>'
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
                //labels: {
                //    type: 'datetime',
                //    step: 1,
                //    rotation: 55,
                //    style: {
                //        fontSize: '11px',
                //        fontFamily: 'IRANSansWeb'
                //    },
                labels: {
                    formatter() {
                        // console.log(getTimeFormat(this.value));
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
            }

        });

        isFirstCallSuccess = true;
    }

    function addChartData(result) {
        if (!isFirstCallSuccess) return;

        if (txnresphisbycha.xAxis[0].categories[txnresphisbycha.xAxis[0].categories.length - 1] != result.labels[0]) {
          
            txnresphisbycha.series[0].addPoint([result.labels[0], result.datasets[0]], true, true);
            txnresphisbycha.series[1].addPoint([result.labels[0], result.datasets[1]], true, true);
            txnresphisbycha.series[2].addPoint([result.labels[0], result.datasets[2]], true, true);
            txnresphisbycha.series[3].addPoint([result.labels[0], result.datasets[3]], true, true);
            /*txnresphisbycha.redraw();*/
            //getTimeFormat(txnresphisbycha.xAxis[0].categories[txnresphisbycha.xAxis[0].categories.length - 1]);

        }



    }
    function toTehranTimezone(date) {
        hourOffset = 4;
        date.setUTCHours(date.getUTCHours(), date.getUTCMinutes());
        //time = date.getTime();
        //date.setUTCFullYear(date.getUTCFullYear());
        //dstStart = date.getTime();
        //date.setUTCFullYear(date.getUTCFullYear());
        //dstEnd = date.getTime();
        //if (time > dstStart && time < dstEnd) hourOffset = 3;
        date.setUTCHours(date.getUTCHours() - hourOffset, date.getUTCMinutes() - 30);
        return date;
    }



    function getTimeFormat(value) {
        var a = toTehranTimezone(new Date(value)).toLocaleTimeString("fa", {
            hour12: false,
            hour: "2-digit",
            minute: "2-digit"
        });
        return (
            a
        );
    }


})
