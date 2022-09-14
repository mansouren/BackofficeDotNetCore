var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("/monitoringHub")
    .withAutomaticReconnect([0, 1000, 5000, 5000, 10000, 10000])
    .build();

connection.start().then(() => console.log("hubconnected")).catch(() => console.log(error));//establish connection

connection.onreconnecting((error) => {
    if (connection.state == signalR.HubConnectionState.Disconnected) {
        isconnected = false;
        connection.Start();
        console.log("Connection is reconnecting : " + error);
    }

});

connection.onreconnected((error) => {
    isconnected = true;
    console.log("Connection is reconnected :" + error);
});



function toTehranTimezone(date) {
    hourOffset = 4;
    date.setUTCHours(date.getUTCHours(), date.getUTCMinutes());
    var now = new Date();
    now.setUTCHours(now.getUTCHours(), now.getUTCMinutes());

    if ((date.getUTCHours() - now.getUTCHours()) < 4) hourOffset = 3;

    date.setUTCHours(date.getUTCHours() - hourOffset, date.getUTCMinutes() - 30);
    return date;
}


function getTimeFormat(value) {

    return (
        toTehranTimezone(new Date(value)).toLocaleTimeString("En", {
            hour12: false,
            hour: "2-digit",
            minute: "2-digit"
        })
    );
}
