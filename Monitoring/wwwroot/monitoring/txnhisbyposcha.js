

var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("/monitoringHub").build();

connection.start().then(() => console.log("hubconnected")).catch(() => console.log(error));//establish connection

connection.on("gettxnhistorybypos", (result) => {
    refreshTxnHistoryByPosChart(JSON.parse(result));
});


function refreshTxnHistoryByPosChart(result) {


    var dataSets = new Array();
    for (i = 0; i < result.datasets.length; i++) {
        var entity = {};
        entity.name = result.datasets[i].label;
        entity.data = result.datasets[i].data;

        entity.type = 'area';
        dataSets.push(entity);

    }

    for (var i in result.datasets) {
        result.datasets[i].pointBorderColor = "#fff";
        switch (result.datasets[i].Key) {
            case 2:
                result.datasets[i].backgroundColor = "rgba(249,114,114,0.2)";
                result.datasets[i].pointBackgroundColor = "rgba(249,114,114,1)";
                result.datasets[i].borderColor = "rgba(249,114,114,1)";
                break;
            case 14:
                result.datasets[i].backgroundColor = "rgba(48,128,20,0.2)";
                result.datasets[i].pointBackgroundColor = "rgba(48,128,20,1)";
                result.datasets[i].borderColor = "rgba(48,128,20,1)";
                break;
            case 5:
                result.datasets[i].backgroundColor = "rgba(0,102,204,0.2)";
                result.datasets[i].pointBackgroundColor = "rgba(0,102,204,1)";
                result.datasets[i].borderColor = "rgba(0,102,204,1)";
                break;
            case 59:
                result.datasets[i].backgroundColor = "rgba(255, 51, 51, 0.2)";
                result.datasets[i].pointBackgroundColor = "rgba(255,51,51,1)";
                result.datasets[i].borderColor = "rgba(255,51,51,1)";
                break;
            case 7:
                result.datasets[i].backgroundColor = "rgba(255,99,0,0.2)";
                result.datasets[i].pointBackgroundColor = "rgba(255,99,0,1)";
                result.datasets[i].borderColor = "rgba(255,99,0,1)";
                break;
            case 37:
                result.datasets[i].backgroundColor = "rgba(192,192,192,0.3)";
                result.datasets[i].pointBackgroundColor = "rgba(192,192,192,1)";
                result.datasets[i].borderColor = "rgba(192,192,192,1)";
                break;
        }
    }

    Highcharts.chart('txnHistoryByPosChart', {
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
            text: 'نمودار تراکنش ابزارهای پذیرش',
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
            labels: {
                type: 'datetime',
                step: 1,
                rotation: 55,
                style: {
                    fontSize: '11px',
                    fontFamily: 'IRANSansWeb'
                },
            }
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
}

