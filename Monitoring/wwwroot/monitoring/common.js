var option = {
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
       /* text: 'نمودار تراکنش کل',*/
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
       /* categories: result.labels,*/
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
    /*series: dataSets,*/
    navigator: {
        enabled: true
    }

}
