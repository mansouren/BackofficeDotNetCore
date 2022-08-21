// Signal R Connection

var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("/monitoringHub").build();

connection.start().then(() => console.log("hubconnected")).catch(() => console.log(error));//establish connection

connection.on("populateData", (product) => { // receive call from StockServiceBackgroundCaller
    refreshchartdata(JSON.parse(product));
});

// chart
var canvas = document.getElementById('myChart').getContext('2d');
var data = {
    labels: [], // empty timeseries for monitor
    datasets: [
        {
            label: "SIgnalR Real Time Item Stock Display using .NetCore IHosted Service",
            fill: false,
            lineTension: 0.0,
            backgroundColor: "rgba(75,192,192,0.4)",
            borderColor: "rgba(75,192,192,1)",
            borderCapStyle: 'butt',
            borderDash: [],
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: "rgba(75,192,192,1)",
            pointBackgroundColor: "#fff",
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            pointHoverBackgroundColor: "rgba(75,192,192,1)",
            pointHoverBorderColor: "rgba(220,220,220,1)",
            pointHoverBorderWidth: 2,
            pointRadius: 5,
            pointHitRadius: 10,
            data: [],// initial empty data
        }
    ]
};

function refreshchartdata(product) {

    if (myLineChart.data.datasets[0].data.length > 6) {// start splicing after some length so that x axis have some data to plot
        myLineChart.data.datasets[0].data.splice(0, 1);
        myLineChart.data.labels.splice(0, 1);
    }
    console.log(product);
    debugger;
    myLineChart.data.datasets[0].data.push(product.stock);
    myLineChart.data.labels.push(product.description.substring(0, 30));
    myLineChart.update();

}


var option = {
    showLines: true,
    scales: {
        y: {
            display: true,
            beginAtZero: true,
        }
    }
};


var myLineChart = new Chart(canvas, {
    type: 'line',
    data: data,
    options: option
});